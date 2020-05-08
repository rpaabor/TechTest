using ClientApplication.ChatService;
using Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        private static string ClientId;
        private List<ClientMessage> clientMessages;

        public Form1()
        {
            ClientId = Guid.NewGuid().ToString();
            InitializeComponent();
            label2.Text = "Del";
            label2.Visible = false;
            label3.Text = "Edit";
            label3.Visible = false;
            this.Text = "Client application";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputBox inputDialog = new InputBox();
            inputDialog.Text = "New message";

            string clientMessage = "";
            if (inputDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using (var client = new ChatService.ChatServiceClient())
                    {
                        clientMessage = inputDialog.textBox1.Text;
                        client.AddMessage(clientMessage, ClientId);
                    }
                    UppdateMessageBox();
                }
                catch (FaultException<Error> fex)
                {
                    var error = ErrorHelper.UnwrapFaultException(fex);
                    MessageBox.Show(error.Message,"Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error");
                }
            }
        }

        private void UppdateMessageBox()
        {
            label2.Visible = true;
            label3.Visible = true;
            try
            {
                using (var client = new ChatServiceClient())
                {
                    clientMessages = client.GetAllMessagesMadeByClient(ClientId).ToList();
                }
                label1.Text = "My sent messages.";
                textBox1.Text = string.Empty;
                int top = 3;
                panel1.Controls.Clear();
                panel2.Controls.Clear();
                foreach (var message in clientMessages)
                {
                    textBox1.Text += message.Message + "\r\n";
                    AddAlterButton(top, message.Id.ToString());
                    top = AddDelButton(top, message.Id.ToString());
                }
            }
            catch (FaultException<Error> fex)
            {
                var error = ErrorHelper.UnwrapFaultException(fex);
                MessageBox.Show(error.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private int AddDelButton(int top, string name)
        {
            int leftPosition = 3;
            var font = new Font(FontFamily.GenericSansSerif, 0.8F);
            Button button = new Button();
            button.Click += new EventHandler(Del_Button_Click);
            button.Name = name;
            button.Top = top;
            button.Left = leftPosition;
            button.BackColor = Color.Red;
            button.Text = "Del";
            button.Font = font;
            button.Size = new Size(36, 16);
            button.Margin = new Padding(3, 0, 3, 0);
            panel1.Controls.Add(button);
            return top += 16;
        }
        private void AddAlterButton(int top, string name)
        {
            int leftPosition = 3;
            var font = new Font(FontFamily.GenericSansSerif, 0.8F);
            Button button = new Button();
            button.Click += new EventHandler(Alter_Button_Click);
            button.Name = name;
            button.Top = top;
            button.Left = leftPosition;
            button.BackColor = Color.Orange;
            button.Text = "Alter";
            button.Font = font;
            button.Size = new Size(36, 16);
            button.Margin = new Padding(3, 0, 3, 0);
            panel2.Controls.Add(button);
        }

        private void Alter_Button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var messageId = button.Name;
            var clientMessage = clientMessages.First(c => c.Id == Guid.Parse(messageId));
            InputBox inputDialog = new InputBox();
            inputDialog.Text = "Edit message";
            inputDialog.textBox1.Text = clientMessage.Message;

            if (inputDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using (var client = new ChatService.ChatServiceClient())
                    {
                        var newMessage = inputDialog.textBox1.Text;
                        client.AlterMessage(newMessage, messageId, ClientId);
                    }
                    UppdateMessageBox();
                }
                catch (FaultException<Error> fex)
                {
                    var error = ErrorHelper.UnwrapFaultException(fex);
                    MessageBox.Show(error.Message, "Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void Del_Button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var messageId = button.Name;
            var messageText = clientMessages.First(c => c.Id == Guid.Parse(messageId)).Message;
            var box = MessageBox.Show(messageText, "Delete", MessageBoxButtons.OKCancel);
            if (box == DialogResult.OK)
            {
                try
                {
                    using (var client = new ChatService.ChatServiceClient())
                    {
                        client.DeleteMessageById(messageId, ClientId);
                    }
                    UppdateMessageBox();
                }
                catch (FaultException<Error> fex)
                {
                    var error = ErrorHelper.UnwrapFaultException(fex);
                    MessageBox.Show(error.Message, "Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new ChatService.ChatServiceClient())
                {
                    label2.Visible = false;
                    label3.Visible = false;
                    panel1.Controls.Clear();
                    panel2.Controls.Clear();
                    var allMessages = client.GetAllMessagesAllClients();
                    textBox1.Text = string.Empty;
                    label1.Text = "All server messages.";
                    foreach (var message in allMessages)
                    {
                        textBox1.Text += message.Message + "\r\n";
                    }
                }
            }
            catch (FaultException<Error> fex)
            {
                var error = ErrorHelper.UnwrapFaultException(fex);
                MessageBox.Show(error.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var client = new ChatService.ChatServiceClient())
            {
                client.ClientDissconnect(ClientId);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UppdateMessageBox();
        }
    }
}
