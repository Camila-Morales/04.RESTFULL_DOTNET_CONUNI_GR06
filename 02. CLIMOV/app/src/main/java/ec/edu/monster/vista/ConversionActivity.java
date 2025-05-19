package ec.edu.monster.vista;

import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import ec.edu.monster.R;
import ec.edu.monster.controlador.ConfigCallback;
import ec.edu.monster.controlador.ConversionControlador;

public class ConversionActivity extends AppCompatActivity {
    private EditText inputTemperature;
    private Spinner spinnerConversion;
    private TextView txtResult;
    private Button btnConvert;
    private ConversionControlador controller;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_conversion);
        // Inicializar controller
        controller = new ConversionControlador();

        initializeViews();
        setupSpinner();
        setupConvertButton();
    }

    private void initializeViews() {
        inputTemperature = findViewById(R.id.inputTemperatura);
        spinnerConversion = findViewById(R.id.spinnerConversion);
        btnConvert = findViewById(R.id.btnConvertir);
        txtResult = findViewById(R.id.txtResultado);
    }

    private void setupSpinner() {
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(
                this,
                R.array.conversion_options,
                android.R.layout.simple_spinner_item
        );
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerConversion.setAdapter(adapter);
    }

    private void setupConvertButton() {
        btnConvert.setOnClickListener(v -> realizarConversion());
    }

    private void realizarConversion() {
        String selectedConversion = spinnerConversion.getSelectedItem().toString();
        String valorTexto = inputTemperature.getText().toString().trim();

        if (valorTexto.isEmpty()) {
            txtResult.setText("Por favor ingrese un valor");
            return;
        }

        try {
            Double valor = Double.parseDouble(valorTexto);

            // Deshabilitar botón y mostrar estado de carga
            btnConvert.setEnabled(false);
            txtResult.setText("Convirtiendo...");

            controller.convertirMedida(
                    selectedConversion,
                    valor,
                    new ConfigCallback<Double>() {
                        @Override
                        public void onSuccess(Double result) {
                            runOnUiThread(() -> {
                                txtResult.setText(String.format("%.2f", result));
                                btnConvert.setEnabled(true);
                            });
                        }

                        @Override
                        public void onError(String error) {
                            runOnUiThread(() -> {
                                txtResult.setText(error);
                                btnConvert.setEnabled(true);
                            });
                        }
                    }
            );
        } catch (NumberFormatException e) {
            txtResult.setText("Por favor ingrese un número válido");
        }
    }
}