import { WeatherApi, Configuration } from '@thread/api-types';

const config = new Configuration({
  basePath: 'http://localhost:5073'
});

const api = new WeatherApi(config);

export async function GET(request: Request) {
  const response = await api.getWeatherForecast();
  return Response.json(response);
}