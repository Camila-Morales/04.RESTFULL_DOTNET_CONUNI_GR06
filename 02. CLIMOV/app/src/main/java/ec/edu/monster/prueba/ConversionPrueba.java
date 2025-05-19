package ec.edu.monster.prueba;

import static org.junit.Assert.assertTrue;

import org.junit.Before;
import org.junit.Test;

import ec.edu.monster.controlador.ConfigCallback;
import ec.edu.monster.controlador.ConversionControlador;

public class ConversionPrueba {
    private ConversionControlador controller;

    @Before
    public void setUp() {
        controller = new ConversionControlador();
    }

    @Test
    public void testMeterToMilesConversion() {
        // Arrange
        Double input = 1024.0;
        final Double[] result = new Double[1];
        final String[] error = new String[1];

        // Act
        controller.convertirMedida("Metros a millas",input, new ConfigCallback<Double>() {
            @Override
            public void onSuccess(Double response) {
                result[0] = response;
            }

            @Override
            public void onError(String errorMessage) {
                error[0] = errorMessage;
            }
        });

        // Esperar brevemente para la respuesta asíncrona
        try {
            Thread.sleep(2000);  // Espera 2 segundos
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        // Assert
        assertTrue("La conversión debería ser aproximadamente 0.636",
                Math.abs(result[0] - 0.636) < 0.001);
    }

    @Test
    public void testInchesToMilesConversion() {
        // Arrange
        Double input = 126720.0;
        final Double[] result = new Double[1];
        final String[] error = new String[1];

        // Act
        controller.convertirMedida("Pulgadas a millas",input, new ConfigCallback<Double>() {
            @Override
            public void onSuccess(Double response) {
                result[0] = response;
            }

            @Override
            public void onError(String errorMessage) {
                error[0] = errorMessage;
            }
        });

        // Esperar brevemente para la respuesta asíncrona
        try {
            Thread.sleep(2000);  // Espera 2 segundos
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        // Assert
        assertTrue("La conversión debería ser aproximadamente 0.636",
                Math.abs(result[0] - 2) < 0.001);
    }
}
