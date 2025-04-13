import { WeatherForecast } from '@thread/api-types';

export async function GET(request: Request) {
  const response = await fetch('http://localhost:5073/weatherforecast');
  const forecasts: WeatherForecast[] = await response.json();
  
  return Response.json(forecasts);
}