
package ec.edu.monster.modelo;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface ApiService {

    @GET("Login/login/{usuario}/{password}")
    Call<Boolean> login(
            @Path("usuario") String usuario,
            @Path("password") String password
    );

    @GET("api/ConversionUnidades/inchesToCm/{inches}")
    Call<Double> convertInchesToCentimeters(@Path("inches") Double inches);

    @GET("api/ConversionUnidades/cmToInches/{cm}")
    Call<Double> convertCentimetersToInches(@Path("cm") Double cm);
}
