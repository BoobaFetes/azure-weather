import {
  Button,
  Grid,
  LinearProgress,
  Pagination,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  useTheme,
} from '@mui/material';
import qs from 'qs';
import { FC, memo, useCallback, useEffect, useMemo, useState } from 'react';

const offset = 5;
export const List: FC = memo(() => {
  const theme = useTheme();
  const { data, error, loading, refetch } = useWeatherForecasts({ offset });
  const [pagination, setPagination] = useState<{
    start?: number;
    offset?: number;
  }>({});

  return (
    <Grid item container spacing={1}>
      <Grid item container direction="row" wrap="nowrap">
        <Grid item container>
          <span style={{ marginRight: theme.spacing(2) }}>
            <Typography>Start</Typography>
            <input
              type="number"
              value={pagination.start}
              onChange={(event) =>
                setPagination({ ...pagination, start: +event.target.value })
              }
            />
          </span>
          <span>
            <Typography>Offset</Typography>
            <input
              type="number"
              value={pagination.offset}
              onChange={(event) =>
                setPagination({ ...pagination, offset: +event.target.value })
              }
            />
          </span>
        </Grid>
        <Grid item>
          <Button
            color="primary"
            variant="contained"
            onClick={() => refetch({ start: undefined, offset: undefined })}
          >
            Refetch all
          </Button>
        </Grid>
      </Grid>
      <Grid item container direction="column" alignItems="center">
        <Typography variant="subtitle2">
          <ul>
            <li>
              {' '}
              Click on table to refetch with start and offset from inputs values
            </li>
            <li> Click n the pagination ({offset} items per pages)</li>
            <li> Click button to fetch all data</li>
          </ul>
        </Typography>
        <Pagination
          style={{ marginBottom: theme.spacing(1) }}
          count={5}
          variant="outlined"
          shape="rounded"
          color="primary"
          onChange={(event, page) =>
            refetch({ start: (page - 1) * 3, offset: 3 })
          }
        />
        <TableContainer
          variant="outlined"
          component={Button}
          onClick={() => refetch(pagination)}
        >
          <Grid>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>id</TableCell>
                  <TableCell>date</TableCell>
                  <TableCell>temperatureC</TableCell>
                  <TableCell>temperatureF</TableCell>
                  <TableCell>summary (id)</TableCell>
                  <TableCell>summary (name)</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {loading && (
                  <TableRow>
                    <TableCell colSpan={6}>
                      <LinearProgress />
                    </TableCell>
                  </TableRow>
                )}
                {error && (
                  <TableRow>
                    <TableCell colSpan={6}>{error.message}</TableCell>
                  </TableRow>
                )}
                {data?.map((item, index) => (
                  <TableRow key={`${item.id}-${index}`}>
                    <TableCell>{item.id}</TableCell>
                    <TableCell>{item.date}</TableCell>
                    <TableCell>{item.temperatureC}</TableCell>
                    <TableCell>{item.temperatureF}</TableCell>
                    <TableCell>{item.summary?.id}</TableCell>
                    <TableCell>{item.summary?.name}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </Grid>
        </TableContainer>
      </Grid>
    </Grid>
  );
});

interface IWeatherForecastDTO {
  id?: number;
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary?: ISummary;
}
function makeWeatherForecast(
  obj?: Partial<IWeatherForecastDTO>
): IWeatherForecastDTO {
  return {
    id: undefined,
    date: '',
    temperatureC: 0,
    temperatureF: 0,
    summary: undefined,
    ...obj,
  };
}

interface ISummary {
  id?: number;
  name: string;
}
function makeSummary(obj?: Partial<ISummary>): ISummary {
  return {
    id: undefined,
    name: '',
    ...obj,
  };
}

interface IUseWeatherForecastsOptions {
  start?: number;
  offset?: number;
}
const useWeatherForecasts = ({
  offset,
  start,
}: IUseWeatherForecastsOptions = {}) => {
  const [data, setData] = useState<IWeatherForecastDTO[] | undefined>();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<Error | undefined>();

  const refetch = useCallback(
    async (options?: IUseWeatherForecastsOptions) => {
      const querystring = qs.stringify({ offset, start, ...options });
      setLoading(true);
      setError(undefined);
      setData(undefined);

      try {
        const response = await fetch(
          `/api/WeatherForecast${!querystring ? '' : `?${querystring}`}`
        );
        const _data = await response.json();
        setData(_data);
      } catch (ex) {
        setError(ex as Error);
      }

      setLoading(false);
    },
    [offset, start]
  );

  useEffect(() => {
    refetch();
  }, [refetch]);

  return useMemo(
    () => ({ data, loading, error, refetch }),
    [data, error, loading, refetch]
  );
};
