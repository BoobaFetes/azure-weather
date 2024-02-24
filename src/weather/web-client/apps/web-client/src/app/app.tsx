// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { Grid, ThemeProvider, Typography } from '@mui/material';
import { WeatherForecast } from './WeatherForecast';
import { theme } from './theme';

export function App() {
  return (
    <ThemeProvider theme={theme}>
      <Grid container direction="row" spacing={2}>
        <Grid item container xs={4} style={{ backgroundColor: 'darkred' }}>
          <Typography variant="body2" color="CaptionText">
            under construction
          </Typography>
        </Grid>
        <Grid item container xs={8}>
          <WeatherForecast.List />
        </Grid>
      </Grid>
    </ThemeProvider>
  );
}

export default App;
