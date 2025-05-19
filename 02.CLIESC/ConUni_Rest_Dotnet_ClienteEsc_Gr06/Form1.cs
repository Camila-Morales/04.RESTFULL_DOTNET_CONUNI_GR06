using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using ConUni_Rest_Dotnet_ClienteEsc_Gr06.ec.edu.monster.models;
using Newtonsoft.Json;

namespace ConUni_Rest_Dotnet_ClienteEsc_Gr06
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Required designer variable (kept for consistency, though not used programmatically).
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // This method is overridden programmatically since we're not using the designer
            SuspendLayout();
            this.AutoScaleDimensions = new SizeF(10F, 25F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 350); // Increased width to accommodate image and controls
            this.Name = "Form1";
            this.Text = "ConUni REST Desktop Client";
            this.Load += new EventHandler(this.Form1_Load);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageLogin;
        private TabPage tabPageConversion;
        private PictureBox pictureBoxLogin; // PictureBox for Login tab
        private PictureBox pictureBoxConversion; // PictureBox for Conversion tab
        private Label labelUsuario;
        private TextBox textBoxUsuario;
        private Label labelContrasena;
        private TextBox textBoxContrasena;
        private Button buttonLogin;
        private Label labelValor;
        private TextBox textBoxValor;
        private Label labelTipo;
        private ComboBox comboBoxTipo;
        private Button buttonConvertir;
        private Label labelResultado;

        private readonly HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
        private readonly HttpClient client;
        private bool isAuthenticated = false;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44364/"); // Adjust port if necessary
        }

        private void InitializeUI()
        {
            // TabControl
            tabControl1 = new TabControl
            {
                Location = new Point(10, 10),
                Size = new Size(580, 300)
            };
            this.Controls.Add(tabControl1);

            // TabPage 1: Login
            tabPageLogin = new TabPage("Login");
            tabControl1.Controls.Add(tabPageLogin);

            // PictureBox for Login tab (left side)
            pictureBoxLogin = new PictureBox
            {
                Location = new Point(20, 20),
                Size = new Size(150, 150),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            try
            {
                pictureBoxLogin.Image = Image.FromFile("monster2.jpg"); // Attempt to load the image
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Image file 'monster2.jpg' not found: {ex.Message}\nPlease ensure the file is in the output directory.");
                pictureBoxLogin.Image = null; // Set to null if not found (optional: use a default image)
            }
            tabPageLogin.Controls.Add(pictureBoxLogin);

            labelUsuario = new Label
            {
                Text = "Username",
                Location = new Point(200, 30),
                AutoSize = true
            };
            tabPageLogin.Controls.Add(labelUsuario);

            textBoxUsuario = new TextBox
            {
                Location = new Point(300, 30),
                Width = 200
            };
            tabPageLogin.Controls.Add(textBoxUsuario);

            labelContrasena = new Label
            {
                Text = "Password",
                Location = new Point(200, 70),
                AutoSize = true
            };
            tabPageLogin.Controls.Add(labelContrasena);

            textBoxContrasena = new TextBox
            {
                Location = new Point(300, 70),
                Width = 200,
                PasswordChar = '*'
            };
            tabPageLogin.Controls.Add(textBoxContrasena);

            buttonLogin = new Button
            {
                Text = "Login",
                Location = new Point(300, 110),
                Width = 100
            };
            buttonLogin.Click += buttonLogin_Click;
            tabPageLogin.Controls.Add(buttonLogin);

            // TabPage 2: Conversion
            tabPageConversion = new TabPage("Conversion")
            {
                Enabled = false
            };
            tabControl1.Controls.Add(tabPageConversion);

            // PictureBox for Conversion tab (left side)
            pictureBoxConversion = new PictureBox
            {
                Location = new Point(20, 20),
                Size = new Size(150, 150),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            try
            {
                pictureBoxConversion.Image = Image.FromFile("monster2.jpg"); // Attempt to load the image
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Image file 'monster2.jpg' not found: {ex.Message}\nPlease ensure the file is in the output directory.");
                pictureBoxConversion.Image = null; // Set to null if not found (optional: use a default image)
            }
            tabPageConversion.Controls.Add(pictureBoxConversion);

            labelValor = new Label
            {
                Text = "Value",
                Location = new Point(200, 30),
                AutoSize = true
            };
            tabPageConversion.Controls.Add(labelValor);

            textBoxValor = new TextBox
            {
                Location = new Point(300, 30),
                Width = 200
            };
            tabPageConversion.Controls.Add(textBoxValor);

            labelTipo = new Label
            {
                Text = "Conversion Type",
                Location = new Point(200, 70),
                AutoSize = true
            };
            tabPageConversion.Controls.Add(labelTipo);

            comboBoxTipo = new ComboBox
            {
                Location = new Point(300, 70),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBoxTipo.Items.AddRange(new string[] { "cm_a_pulgadas", "pulgadas_a_cm" });
            tabPageConversion.Controls.Add(comboBoxTipo);

            buttonConvertir = new Button
            {
                Text = "Convert",
                Location = new Point(300, 110),
                Width = 100
            };
            buttonConvertir.Click += buttonConvertir_Click;
            tabPageConversion.Controls.Add(buttonConvertir);

            labelResultado = new Label
            {
                Text = "Result: ",
                Location = new Point(200, 150),
                AutoSize = true
            };
            tabPageConversion.Controls.Add(labelResultado);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Empty for now; add initialization logic if needed
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            var loginModel = new Login
            {
                Usuario = textBoxUsuario.Text,
                Contrasena = textBoxContrasena.Text
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("api/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic result = JsonConvert.DeserializeObject(responseContent);
                    MessageBox.Show(result.mensaje.ToString());
                    isAuthenticated = true;
                    tabPageConversion.Enabled = true;
                    tabControl1.SelectedIndex = 1;
                }
                else
                {
                    MessageBox.Show($"Login failed: {responseContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}\nEnsure the server is running and the port is correct.");
            }
        }

        private async void buttonConvertir_Click(object sender, EventArgs e)
        {
            if (!isAuthenticated)
            {
                MessageBox.Show("You must log in first.");
                return;
            }

            if (!double.TryParse(textBoxValor.Text, out double valor))
            {
                MessageBox.Show("Invalid value. It must be a number.");
                return;
            }

            if (comboBoxTipo.SelectedItem == null)
            {
                MessageBox.Show("Select a conversion type.");
                return;
            }

            var conversionModel = new Conversion
            {
                Valor = valor,
                Tipo = comboBoxTipo.SelectedItem.ToString()
            };

            var content = new StringContent(JsonConvert.SerializeObject(conversionModel), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("api/conuni", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic result = JsonConvert.DeserializeObject(responseContent);
                    labelResultado.Text = $"Result: {result.resultado}";
                }
                else
                {
                    MessageBox.Show($"Error: {responseContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}");
            }
        }
    }
}