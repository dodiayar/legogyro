using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lego.Ev3.Core;
using Lego.Ev3.Desktop;

namespace Gyroscope
{
    public partial class Form1 : Form
    {
        Brick _brick;
        InputPort inputPort;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _brick = new Brick(new UsbCommunication());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputPort = InputPort.One;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputPort = InputPort.Two;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inputPort = InputPort.Three;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            inputPort = InputPort.Four;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            
        }

        private async void _brick_BrickChanged(object sender, BrickChangedEventArgs e)
        {
            label2.Text = "Gyroscope Value" + e.Ports[inputPort].SIValue;
            if(e.Ports[inputPort].SIValue < 0)
            {
                _brick.BatchCommand.TurnMotorAtPower(OutputPort.A, 100);
                _brick.BatchCommand.TurnMotorAtSpeed(OutputPort.B, 95);
                label3.Text = "Go Right";
                await _brick.BatchCommand.SendCommandAsync();
            }
            else if(e.Ports[inputPort].SIValue > 0)
            {
                _brick.BatchCommand.TurnMotorAtPower(OutputPort.A, 95);
                _brick.BatchCommand.TurnMotorAtPower(OutputPort.B, 100);
                label3.Text = "Go Left";
                await _brick.BatchCommand.SendCommandAsync();
            }
            else if(e.Ports[inputPort].SIValue == 0)
            {
                await _brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A | OutputPort.B, 100);
                label3.Text = "Go Straight";
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await _brick.ConnectAsync();
            _brick.BrickChanged += _brick_BrickChanged;
            await _brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A | OutputPort.B, 100);
        }
    }
}
