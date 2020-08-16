namespace leitor_mdb_access
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_buscar = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView_estacao = new System.Windows.Forms.DataGridView();
            this.Radio_Chuva = new System.Windows.Forms.RadioButton();
            this.Radio_Nivel = new System.Windows.Forms.RadioButton();
            this.Radio_Vazao = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_mdb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_codEstacao = new System.Windows.Forms.TextBox();
            this.btn_gerar = new System.Windows.Forms.Button();
            this.dtPicker_inicio = new System.Windows.Forms.DateTimePicker();
            this.dtPicker_fim = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_estacao)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(361, 28);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 0;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.Btn_buscar_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Bruto",
            "Consistido",
            "Ambos"});
            this.comboBox1.Location = new System.Drawing.Point(12, 209);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(114, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "Ambos";
            // 
            // dataGridView_estacao
            // 
            this.dataGridView_estacao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_estacao.Location = new System.Drawing.Point(12, 59);
            this.dataGridView_estacao.Name = "dataGridView_estacao";
            this.dataGridView_estacao.ReadOnly = true;
            this.dataGridView_estacao.Size = new System.Drawing.Size(424, 131);
            this.dataGridView_estacao.TabIndex = 2;
            this.dataGridView_estacao.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_estacao_CellClick);
            // 
            // Radio_Chuva
            // 
            this.Radio_Chuva.AutoSize = true;
            this.Radio_Chuva.Location = new System.Drawing.Point(147, 209);
            this.Radio_Chuva.Name = "Radio_Chuva";
            this.Radio_Chuva.Size = new System.Drawing.Size(56, 17);
            this.Radio_Chuva.TabIndex = 1;
            this.Radio_Chuva.Text = "Chuva";
            this.Radio_Chuva.UseVisualStyleBackColor = true;
            this.Radio_Chuva.CheckedChanged += new System.EventHandler(this.Radio_Chuva_CheckedChanged);
            // 
            // Radio_Nivel
            // 
            this.Radio_Nivel.AutoSize = true;
            this.Radio_Nivel.Location = new System.Drawing.Point(147, 232);
            this.Radio_Nivel.Name = "Radio_Nivel";
            this.Radio_Nivel.Size = new System.Drawing.Size(49, 17);
            this.Radio_Nivel.TabIndex = 2;
            this.Radio_Nivel.Text = "Nivel";
            this.Radio_Nivel.UseVisualStyleBackColor = true;
            this.Radio_Nivel.CheckedChanged += new System.EventHandler(this.Radio_Nivel_CheckedChanged);
            // 
            // Radio_Vazao
            // 
            this.Radio_Vazao.AutoSize = true;
            this.Radio_Vazao.Location = new System.Drawing.Point(148, 255);
            this.Radio_Vazao.Name = "Radio_Vazao";
            this.Radio_Vazao.Size = new System.Drawing.Size(55, 17);
            this.Radio_Vazao.TabIndex = 3;
            this.Radio_Vazao.Text = "Vazão";
            this.Radio_Vazao.UseVisualStyleBackColor = true;
            this.Radio_Vazao.CheckedChanged += new System.EventHandler(this.Radio_Vazao_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Data Início";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Data Fim";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tipo de dado";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Variável";
            // 
            // textBox_mdb
            // 
            this.textBox_mdb.Enabled = false;
            this.textBox_mdb.Location = new System.Drawing.Point(12, 31);
            this.textBox_mdb.Name = "textBox_mdb";
            this.textBox_mdb.Size = new System.Drawing.Size(343, 20);
            this.textBox_mdb.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Local documento";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Código Estação";
            // 
            // textBox_codEstacao
            // 
            this.textBox_codEstacao.Location = new System.Drawing.Point(232, 210);
            this.textBox_codEstacao.Name = "textBox_codEstacao";
            this.textBox_codEstacao.Size = new System.Drawing.Size(204, 20);
            this.textBox_codEstacao.TabIndex = 13;
            this.textBox_codEstacao.TextChanged += new System.EventHandler(this.TextBox_codEstacao_TextChanged);
            // 
            // btn_gerar
            // 
            this.btn_gerar.Location = new System.Drawing.Point(232, 300);
            this.btn_gerar.Name = "btn_gerar";
            this.btn_gerar.Size = new System.Drawing.Size(204, 20);
            this.btn_gerar.TabIndex = 14;
            this.btn_gerar.Text = "Gerar série histórica";
            this.btn_gerar.UseVisualStyleBackColor = true;
            this.btn_gerar.Click += new System.EventHandler(this.Btn_gerar_Click);
            // 
            // dtPicker_inicio
            // 
            this.dtPicker_inicio.Location = new System.Drawing.Point(16, 302);
            this.dtPicker_inicio.Name = "dtPicker_inicio";
            this.dtPicker_inicio.Size = new System.Drawing.Size(79, 20);
            this.dtPicker_inicio.TabIndex = 15;
            // 
            // dtPicker_fim
            // 
            this.dtPicker_fim.Location = new System.Drawing.Point(147, 302);
            this.dtPicker_fim.Name = "dtPicker_fim";
            this.dtPicker_fim.Size = new System.Drawing.Size(79, 20);
            this.dtPicker_fim.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 334);
            this.Controls.Add(this.dtPicker_fim);
            this.Controls.Add(this.dtPicker_inicio);
            this.Controls.Add(this.btn_gerar);
            this.Controls.Add(this.textBox_codEstacao);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_mdb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Radio_Vazao);
            this.Controls.Add(this.Radio_Nivel);
            this.Controls.Add(this.Radio_Chuva);
            this.Controls.Add(this.dataGridView_estacao);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_buscar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Leitor_banco_access_ANA";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_estacao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView_estacao;
        private System.Windows.Forms.RadioButton Radio_Chuva;
        private System.Windows.Forms.RadioButton Radio_Nivel;
        private System.Windows.Forms.RadioButton Radio_Vazao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_mdb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_codEstacao;
        private System.Windows.Forms.Button btn_gerar;
        private System.Windows.Forms.DateTimePicker dtPicker_inicio;
        private System.Windows.Forms.DateTimePicker dtPicker_fim;
    }
}

