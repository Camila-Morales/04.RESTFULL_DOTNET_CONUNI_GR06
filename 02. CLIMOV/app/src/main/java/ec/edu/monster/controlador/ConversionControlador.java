package ec.edu.monster.controlador;

import ec.edu.monster.modelo.ApiService;
import ec.edu.monster.modelo.Config;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ConversionControlador {
    private static final String BASE_URL = Config.BASE_URL;
    private ApiService apiService;

    public ConversionControlador() {
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        apiService = retrofit.create(ApiService.class);
    }

    public void convertirMedida(String tipoConversion, Double valor, ConfigCallback<Double> callback) {
        Call<Double> call;

        switch (tipoConversion) {
            case "Pulgadas a centímetros":
                call = apiService.convertInchesToCentimeters(valor);
                break;
            case "Centímetros a pulgadas":
                call = apiService.convertCentimetersToInches(valor);
                break;
            default:
                callback.onError("Tipo de conversión no soportado");
                return;
        }

        call.enqueue(new Callback<Double>() {
            @Override
            public void onResponse(Call<Double> call, Response<Double> response) {
                if (response.isSuccessful() && response.body() != null) {
                    callback.onSuccess(response.body());
                } else {
                    callback.onError("Error en servicio de conversión");
                }
            }

            @Override
            public void onFailure(Call<Double> call, Throwable t) {
                callback.onError("Error de conexión: " + t.getMessage());
            }
        });
    }

}
