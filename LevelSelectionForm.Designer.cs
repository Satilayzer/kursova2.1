using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

public class LevelSelectionForm : Form
{
    private Game game;

    public LevelSelectionForm()
    {
        this.game = null;
        InitializeComponents();
        this.Size = new System.Drawing.Size(70, 150);
    }

    class GameCreatorButton: Button
    {
        public GameCreatorButton(LevelSelectionForm parent, string name, int gameSize) : base()
        {
            this.parent = parent;
            this.gameSize = gameSize;
            this.Text = name;
            this.Click += createGame;
            //this.Size = new System.Drawing.Size(150, 100);
        }

        LevelSelectionForm parent;
        int gameSize;

        public void createGame(object sender, EventArgs e)
        {
            Game game = new Game(gameSize);
            parent.Hide();
            CardMatchingForm matchingForm = new CardMatchingForm(game, parent);
            matchingForm.Show();
        }
    }

    private void InitializeComponents()
    {
        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
        flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        flowLayoutPanel.Controls.Add(new GameCreatorButton(this, "Рiвень 1", 4));
        flowLayoutPanel.Controls.Add(new GameCreatorButton(this, "Рiвень 2", 6));
        flowLayoutPanel.Controls.Add(new GameCreatorButton(this, "Рiвень 3", 8));


        Controls.Add(flowLayoutPanel);
    }
}
