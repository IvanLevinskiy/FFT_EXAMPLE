
namespace FTT_ARDUINO
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStream = new System.Windows.Forms.Button();
            this.cbxPorts = new System.Windows.Forms.ComboBox();
            this.zgFrequency = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // buttonStream
            // 
            this.buttonStream.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStream.Enabled = false;
            this.buttonStream.Location = new System.Drawing.Point(145, 487);
            this.buttonStream.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonStream.Name = "buttonStream";
            this.buttonStream.Size = new System.Drawing.Size(108, 22);
            this.buttonStream.TabIndex = 1;
            this.buttonStream.Text = "Stream";
            this.buttonStream.UseVisualStyleBackColor = true;
            this.buttonStream.Click += new System.EventHandler(this.buttonStream_Click);
            // 
            // cbxPorts
            // 
            this.cbxPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxPorts.FormattingEnabled = true;
            this.cbxPorts.Location = new System.Drawing.Point(9, 488);
            this.cbxPorts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxPorts.Name = "cbxPorts";
            this.cbxPorts.Size = new System.Drawing.Size(132, 21);
            this.cbxPorts.TabIndex = 3;
            this.cbxPorts.SelectedValueChanged += new System.EventHandler(this.cbxPorts_SelectedValueChanged);
            this.cbxPorts.Click += new System.EventHandler(this.cbxPorts_Click);
            // 
            // zgFrequency
            // 
            this.zgFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zgFrequency.Location = new System.Drawing.Point(12, 12);
            this.zgFrequency.Name = "zgFrequency";
            this.zgFrequency.ScrollGrace = 0D;
            this.zgFrequency.ScrollMaxX = 0D;
            this.zgFrequency.ScrollMaxY = 0D;
            this.zgFrequency.ScrollMaxY2 = 0D;
            this.zgFrequency.ScrollMinX = 0D;
            this.zgFrequency.ScrollMinY = 0D;
            this.zgFrequency.ScrollMinY2 = 0D;
            this.zgFrequency.Size = new System.Drawing.Size(844, 470);
            this.zgFrequency.TabIndex = 0;
            this.zgFrequency.UseExtendedPrintDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 510);
            this.Controls.Add(this.zgFrequency);
            this.Controls.Add(this.cbxPorts);
            this.Controls.Add(this.buttonStream);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonStream;
        private System.Windows.Forms.ComboBox cbxPorts;
        private ZedGraph.ZedGraphControl zgFrequency;
    }
}

