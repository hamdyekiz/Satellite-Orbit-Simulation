namespace Space_Simulation
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("Satellites", 0, 0);
            TreeNode treeNode2 = new TreeNode("Ground Stations", 2, 2);
            TreeNode treeNode3 = new TreeNode("Space System", 1, 1, new TreeNode[] { treeNode1, treeNode2 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            textBoxOrbitRadius = new TextBox();
            textBoxThetaAngle = new TextBox();
            textBoxPhiAngle = new TextBox();
            textBoxMass = new TextBox();
            textBoxVelocity = new TextBox();
            treeViewSpaceSystem = new TreeView();
            ımageList1 = new ImageList(components);
            panelSatellite = new Panel();
            panelSatellitePositionTrajectoryViewer = new Panel();
            panelSatellitePositionTrajectoryController = new Panel();
            labelPositionGraph = new Label();
            buttonSavePosition = new Button();
            buttonClosePosition = new Button();
            panel7 = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            panel4 = new Panel();
            pictureBoxSatelliteTrajectoryViewer = new PictureBox();
            panelMainPage = new Panel();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            buttonShowVelocityGraph = new Button();
            buttonShowPositionGraph = new Button();
            panelSatelliteVelocityTrajectoryViewer = new Panel();
            panel9 = new Panel();
            labelVelocityGraph = new Label();
            buttonSaveVelocity = new Button();
            buttonCloseVelocity = new Button();
            panel10 = new Panel();
            panel11 = new Panel();
            panel12 = new Panel();
            panel13 = new Panel();
            pictureBoxSatelliteVelocityTrajectoryViewer = new PictureBox();
            textBoxSatelliteName = new TextBox();
            labelSatelliteName = new Label();
            panel1 = new Panel();
            buttonGetSatelliteReport = new Button();
            radioButtonSS = new RadioButton();
            buttonSaveToFileSatellite = new Button();
            buttonOpenFromFileSatellite = new Button();
            radioButtonRS = new RadioButton();
            buttonUpdateSatellite = new Button();
            buttonCreateSatellite = new Button();
            labelOrbitRadius = new Label();
            labelThetaAngle = new Label();
            labelVelocity = new Label();
            labelMass = new Label();
            labelFiAngle = new Label();
            labelSatelliteType = new Label();
            pictureBoxSatellite = new PictureBox();
            pictureBoxWorld = new PictureBox();
            pictureBoxSatelliteViewer = new PictureBox();
            buttonStartSimulation = new Button();
            labelTimeStep = new Label();
            labelSimulationTime = new Label();
            textBoxSimulationTime = new TextBox();
            textBoxTimeStep = new TextBox();
            panelGroundStation = new Panel();
            labelGroundStationName = new Label();
            textBoxGroundStationName = new TextBox();
            pictureBoxGroundStation = new PictureBox();
            pictureBoxWorld_2 = new PictureBox();
            pictureBoxGroundStationViewer = new PictureBox();
            panel3 = new Panel();
            buttonGetGroundStationReport = new Button();
            radioButtonBCT_GroundStation = new RadioButton();
            radioButtonT_GroundStation = new RadioButton();
            buttonSaveToFileGS = new Button();
            buttonOpenFromFileGS = new Button();
            buttonUpdateGroundStation = new Button();
            radioButtonC_GroundStation = new RadioButton();
            textBoxGroundStationFoVAngle = new TextBox();
            textBoxGroundStationTheta = new TextBox();
            labelGroundStationTheta = new Label();
            labelGroundStationFoVAngle = new Label();
            labelGroundStationType = new Label();
            buttonCreateGroundStation = new Button();
            progressBarSimulation = new ProgressBar();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            radioButtonEuler = new RadioButton();
            radioButtonRungeKutta = new RadioButton();
            labelSimulationType = new Label();
            labelProgressBar = new Label();
            panelSatellite.SuspendLayout();
            panelSatellitePositionTrajectoryViewer.SuspendLayout();
            panelSatellitePositionTrajectoryController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSatelliteTrajectoryViewer).BeginInit();
            panelMainPage.SuspendLayout();
            panelSatelliteVelocityTrajectoryViewer.SuspendLayout();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSatelliteVelocityTrajectoryViewer).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSatellite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWorld).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSatelliteViewer).BeginInit();
            panelGroundStation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGroundStation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWorld_2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGroundStationViewer).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBoxOrbitRadius
            // 
            textBoxOrbitRadius.Location = new Point(25, 115);
            textBoxOrbitRadius.Name = "textBoxOrbitRadius";
            textBoxOrbitRadius.Size = new Size(100, 23);
            textBoxOrbitRadius.TabIndex = 0;
            // 
            // textBoxThetaAngle
            // 
            textBoxThetaAngle.Location = new Point(25, 175);
            textBoxThetaAngle.Name = "textBoxThetaAngle";
            textBoxThetaAngle.Size = new Size(100, 23);
            textBoxThetaAngle.TabIndex = 1;
            textBoxThetaAngle.TextChanged += textBoxThetaAngle_TextChanged;
            // 
            // textBoxPhiAngle
            // 
            textBoxPhiAngle.Location = new Point(25, 240);
            textBoxPhiAngle.Name = "textBoxPhiAngle";
            textBoxPhiAngle.Size = new Size(100, 23);
            textBoxPhiAngle.TabIndex = 2;
            textBoxPhiAngle.TextChanged += textBoxPhiAngle_TextChanged;
            // 
            // textBoxMass
            // 
            textBoxMass.Location = new Point(25, 305);
            textBoxMass.Name = "textBoxMass";
            textBoxMass.Size = new Size(100, 23);
            textBoxMass.TabIndex = 3;
            // 
            // textBoxVelocity
            // 
            textBoxVelocity.Location = new Point(25, 370);
            textBoxVelocity.Name = "textBoxVelocity";
            textBoxVelocity.Size = new Size(100, 23);
            textBoxVelocity.TabIndex = 4;
            // 
            // treeViewSpaceSystem
            // 
            treeViewSpaceSystem.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            treeViewSpaceSystem.ImageIndex = 0;
            treeViewSpaceSystem.ImageList = ımageList1;
            treeViewSpaceSystem.ItemHeight = 20;
            treeViewSpaceSystem.Location = new Point(10, 80);
            treeViewSpaceSystem.Name = "treeViewSpaceSystem";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "Satellite";
            treeNode1.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "Satellites";
            treeNode2.ImageIndex = 2;
            treeNode2.Name = "GroundStation";
            treeNode2.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            treeNode2.SelectedImageIndex = 2;
            treeNode2.Text = "Ground Stations";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "SpaceSystem";
            treeNode3.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "Space System";
            treeViewSpaceSystem.Nodes.AddRange(new TreeNode[] { treeNode3 });
            treeViewSpaceSystem.SelectedImageIndex = 0;
            treeViewSpaceSystem.Size = new Size(180, 585);
            treeViewSpaceSystem.TabIndex = 8;
            treeViewSpaceSystem.AfterSelect += treeViewSpaceSystem_AfterSelect;
            // 
            // ımageList1
            // 
            ımageList1.ColorDepth = ColorDepth.Depth32Bit;
            ımageList1.ImageStream = (ImageListStreamer)resources.GetObject("ımageList1.ImageStream");
            ımageList1.TransparentColor = Color.Transparent;
            ımageList1.Images.SetKeyName(0, "icons8-satellite-60 (1).png");
            ımageList1.Images.SetKeyName(1, "icons8-solar-system-24.png");
            ımageList1.Images.SetKeyName(2, "icons8-satellite-64 (2) (1).png");
            // 
            // panelSatellite
            // 
            panelSatellite.BackColor = SystemColors.Window;
            panelSatellite.BorderStyle = BorderStyle.FixedSingle;
            panelSatellite.Controls.Add(panelSatellitePositionTrajectoryViewer);
            panelSatellite.Controls.Add(buttonShowVelocityGraph);
            panelSatellite.Controls.Add(buttonShowPositionGraph);
            panelSatellite.Controls.Add(panelSatelliteVelocityTrajectoryViewer);
            panelSatellite.Controls.Add(textBoxSatelliteName);
            panelSatellite.Controls.Add(labelSatelliteName);
            panelSatellite.Controls.Add(panel1);
            panelSatellite.Controls.Add(pictureBoxSatellite);
            panelSatellite.Controls.Add(pictureBoxWorld);
            panelSatellite.Controls.Add(pictureBoxSatelliteViewer);
            panelSatellite.Location = new Point(200, 80);
            panelSatellite.Name = "panelSatellite";
            panelSatellite.Size = new Size(770, 585);
            panelSatellite.TabIndex = 9;
            panelSatellite.Visible = false;
            // 
            // panelSatellitePositionTrajectoryViewer
            // 
            panelSatellitePositionTrajectoryViewer.BackColor = Color.White;
            panelSatellitePositionTrajectoryViewer.Controls.Add(panelSatellitePositionTrajectoryController);
            panelSatellitePositionTrajectoryViewer.Controls.Add(panel7);
            panelSatellitePositionTrajectoryViewer.Controls.Add(panel6);
            panelSatellitePositionTrajectoryViewer.Controls.Add(panel5);
            panelSatellitePositionTrajectoryViewer.Controls.Add(panel4);
            panelSatellitePositionTrajectoryViewer.Controls.Add(pictureBoxSatelliteTrajectoryViewer);
            panelSatellitePositionTrajectoryViewer.Location = new Point(20, 50);
            panelSatellitePositionTrajectoryViewer.Name = "panelSatellitePositionTrajectoryViewer";
            panelSatellitePositionTrajectoryViewer.Size = new Size(520, 470);
            panelSatellitePositionTrajectoryViewer.TabIndex = 13;
            panelSatellitePositionTrajectoryViewer.Visible = false;
            // 
            // panelSatellitePositionTrajectoryController
            // 
            panelSatellitePositionTrajectoryController.BackColor = Color.White;
            panelSatellitePositionTrajectoryController.Controls.Add(labelPositionGraph);
            panelSatellitePositionTrajectoryController.Controls.Add(buttonSavePosition);
            panelSatellitePositionTrajectoryController.Controls.Add(buttonClosePosition);
            panelSatellitePositionTrajectoryController.Location = new Point(10, 14);
            panelSatellitePositionTrajectoryController.Name = "panelSatellitePositionTrajectoryController";
            panelSatellitePositionTrajectoryController.Size = new Size(500, 50);
            panelSatellitePositionTrajectoryController.TabIndex = 30;
            // 
            // labelPositionGraph
            // 
            labelPositionGraph.AutoSize = true;
            labelPositionGraph.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelPositionGraph.Location = new Point(140, 10);
            labelPositionGraph.Name = "labelPositionGraph";
            labelPositionGraph.Size = new Size(65, 25);
            labelPositionGraph.TabIndex = 3;
            labelPositionGraph.Text = "label1";
            // 
            // buttonSavePosition
            // 
            buttonSavePosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonSavePosition.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonSavePosition.Image = (Image)resources.GetObject("buttonSavePosition.Image");
            buttonSavePosition.Location = new Point(10, 10);
            buttonSavePosition.Name = "buttonSavePosition";
            buttonSavePosition.Size = new Size(110, 30);
            buttonSavePosition.TabIndex = 2;
            buttonSavePosition.Text = "Save Image";
            buttonSavePosition.TextAlign = ContentAlignment.MiddleLeft;
            buttonSavePosition.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSavePosition.UseVisualStyleBackColor = true;
            buttonSavePosition.Click += buttonSavePosition_Click;
            // 
            // buttonClosePosition
            // 
            buttonClosePosition.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonClosePosition.Image = (Image)resources.GetObject("buttonClosePosition.Image");
            buttonClosePosition.Location = new Point(410, 10);
            buttonClosePosition.Name = "buttonClosePosition";
            buttonClosePosition.Size = new Size(80, 30);
            buttonClosePosition.TabIndex = 1;
            buttonClosePosition.Text = "Close";
            buttonClosePosition.TextAlign = ContentAlignment.MiddleLeft;
            buttonClosePosition.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonClosePosition.UseVisualStyleBackColor = true;
            buttonClosePosition.Click += buttonClosePosition_Click;
            // 
            // panel7
            // 
            panel7.BackColor = Color.DarkSalmon;
            panel7.Dock = DockStyle.Bottom;
            panel7.Location = new Point(4, 466);
            panel7.Name = "panel7";
            panel7.Size = new Size(512, 4);
            panel7.TabIndex = 33;
            // 
            // panel6
            // 
            panel6.BackColor = Color.DarkSalmon;
            panel6.Dock = DockStyle.Right;
            panel6.Location = new Point(516, 4);
            panel6.Name = "panel6";
            panel6.Size = new Size(4, 466);
            panel6.TabIndex = 33;
            // 
            // panel5
            // 
            panel5.BackColor = Color.DarkSalmon;
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(4, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(516, 4);
            panel5.TabIndex = 32;
            // 
            // panel4
            // 
            panel4.BackColor = Color.DarkSalmon;
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(4, 470);
            panel4.TabIndex = 31;
            // 
            // pictureBoxSatelliteTrajectoryViewer
            // 
            pictureBoxSatelliteTrajectoryViewer.BackColor = Color.White;
            pictureBoxSatelliteTrajectoryViewer.Location = new Point(10, 70);
            pictureBoxSatelliteTrajectoryViewer.Name = "pictureBoxSatelliteTrajectoryViewer";
            pictureBoxSatelliteTrajectoryViewer.Size = new Size(500, 400);
            pictureBoxSatelliteTrajectoryViewer.TabIndex = 29;
            pictureBoxSatelliteTrajectoryViewer.TabStop = false;
            pictureBoxSatelliteTrajectoryViewer.Paint += drawing_pictureBoxSatelliteTrajectoryViewer;
            // 
            // panelMainPage
            // 
            panelMainPage.BackColor = SystemColors.Window;
            panelMainPage.BackgroundImage = (Image)resources.GetObject("panelMainPage.BackgroundImage");
            panelMainPage.Controls.Add(label7);
            panelMainPage.Controls.Add(label6);
            panelMainPage.Controls.Add(label5);
            panelMainPage.Controls.Add(label4);
            panelMainPage.Controls.Add(label3);
            panelMainPage.Controls.Add(label2);
            panelMainPage.Controls.Add(label1);
            panelMainPage.Location = new Point(200, 80);
            panelMainPage.Name = "panelMainPage";
            panelMainPage.Size = new Size(770, 585);
            panelMainPage.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label7.ForeColor = SystemColors.ButtonFace;
            label7.Image = (Image)resources.GetObject("label7.Image");
            label7.Location = new Point(214, 523);
            label7.Name = "label7";
            label7.Size = new Size(349, 32);
            label7.TabIndex = 6;
            label7.Text = "Code Written By: Hamdi EKIZ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label6.ForeColor = SystemColors.ButtonFace;
            label6.Image = (Image)resources.GetObject("label6.Image");
            label6.Location = new Point(13, 144);
            label6.Name = "label6";
            label6.Size = new Size(752, 47);
            label6.TabIndex = 5;
            label6.Text = "MECHANICAL ENGINEERING  DEPARTMENT";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label5.ForeColor = SystemColors.ButtonFace;
            label5.Image = (Image)resources.GetObject("label5.Image");
            label5.Location = new Point(155, 87);
            label5.Name = "label5";
            label5.Size = new Size(490, 50);
            label5.TabIndex = 4;
            label5.Text = "FACULTY OF ENGINEERING";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label4.ForeColor = SystemColors.ButtonFace;
            label4.Image = (Image)resources.GetObject("label4.Image");
            label4.Location = new Point(14, 9);
            label4.Name = "label4";
            label4.Size = new Size(740, 65);
            label4.TabIndex = 3;
            label4.Text = "GEBZE TECHNICAL UNIVERSITY";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Image = (Image)resources.GetObject("label3.Image");
            label3.Location = new Point(236, 310);
            label3.Name = "label3";
            label3.Size = new Size(289, 50);
            label3.TabIndex = 2;
            label3.Text = "TERM PROJECT";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Image = (Image)resources.GetObject("label2.Image");
            label2.Location = new Point(65, 265);
            label2.Name = "label2";
            label2.Size = new Size(649, 50);
            label2.TabIndex = 1;
            label2.Text = "ADVANCED PROGRAMMING ME108";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.Location = new Point(28, 360);
            label1.Name = "label1";
            label1.Size = new Size(723, 50);
            label1.TabIndex = 0;
            label1.Text = "SPACE-SYSTEM SIMULATION PROGRAM";
            // 
            // buttonShowVelocityGraph
            // 
            buttonShowVelocityGraph.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonShowVelocityGraph.Image = (Image)resources.GetObject("buttonShowVelocityGraph.Image");
            buttonShowVelocityGraph.Location = new Point(290, 550);
            buttonShowVelocityGraph.Name = "buttonShowVelocityGraph";
            buttonShowVelocityGraph.Size = new Size(120, 30);
            buttonShowVelocityGraph.TabIndex = 30;
            buttonShowVelocityGraph.Text = "Velocity Graph";
            buttonShowVelocityGraph.TextAlign = ContentAlignment.MiddleLeft;
            buttonShowVelocityGraph.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonShowVelocityGraph.UseVisualStyleBackColor = true;
            buttonShowVelocityGraph.Click += buttonShowVelocityGraph_Click;
            // 
            // buttonShowPositionGraph
            // 
            buttonShowPositionGraph.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonShowPositionGraph.Image = (Image)resources.GetObject("buttonShowPositionGraph.Image");
            buttonShowPositionGraph.ImageAlign = ContentAlignment.MiddleLeft;
            buttonShowPositionGraph.Location = new Point(150, 550);
            buttonShowPositionGraph.Name = "buttonShowPositionGraph";
            buttonShowPositionGraph.Size = new Size(120, 30);
            buttonShowPositionGraph.TabIndex = 29;
            buttonShowPositionGraph.Text = "Position Graph";
            buttonShowPositionGraph.TextAlign = ContentAlignment.MiddleLeft;
            buttonShowPositionGraph.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonShowPositionGraph.UseVisualStyleBackColor = true;
            buttonShowPositionGraph.Click += buttonShowPositionGraph_Click;
            // 
            // panelSatelliteVelocityTrajectoryViewer
            // 
            panelSatelliteVelocityTrajectoryViewer.BackColor = Color.White;
            panelSatelliteVelocityTrajectoryViewer.Controls.Add(panel9);
            panelSatelliteVelocityTrajectoryViewer.Controls.Add(panel10);
            panelSatelliteVelocityTrajectoryViewer.Controls.Add(panel11);
            panelSatelliteVelocityTrajectoryViewer.Controls.Add(panel12);
            panelSatelliteVelocityTrajectoryViewer.Controls.Add(panel13);
            panelSatelliteVelocityTrajectoryViewer.Controls.Add(pictureBoxSatelliteVelocityTrajectoryViewer);
            panelSatelliteVelocityTrajectoryViewer.Location = new Point(20, 50);
            panelSatelliteVelocityTrajectoryViewer.Name = "panelSatelliteVelocityTrajectoryViewer";
            panelSatelliteVelocityTrajectoryViewer.Size = new Size(520, 470);
            panelSatelliteVelocityTrajectoryViewer.TabIndex = 34;
            panelSatelliteVelocityTrajectoryViewer.Visible = false;
            // 
            // panel9
            // 
            panel9.BackColor = Color.White;
            panel9.Controls.Add(labelVelocityGraph);
            panel9.Controls.Add(buttonSaveVelocity);
            panel9.Controls.Add(buttonCloseVelocity);
            panel9.Location = new Point(10, 14);
            panel9.Name = "panel9";
            panel9.Size = new Size(500, 50);
            panel9.TabIndex = 30;
            // 
            // labelVelocityGraph
            // 
            labelVelocityGraph.AutoSize = true;
            labelVelocityGraph.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelVelocityGraph.Location = new Point(140, 10);
            labelVelocityGraph.Name = "labelVelocityGraph";
            labelVelocityGraph.Size = new Size(65, 25);
            labelVelocityGraph.TabIndex = 3;
            labelVelocityGraph.Text = "label1";
            // 
            // buttonSaveVelocity
            // 
            buttonSaveVelocity.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonSaveVelocity.Image = (Image)resources.GetObject("buttonSaveVelocity.Image");
            buttonSaveVelocity.Location = new Point(10, 10);
            buttonSaveVelocity.Name = "buttonSaveVelocity";
            buttonSaveVelocity.Size = new Size(110, 30);
            buttonSaveVelocity.TabIndex = 2;
            buttonSaveVelocity.Text = "Save Image";
            buttonSaveVelocity.TextAlign = ContentAlignment.MiddleLeft;
            buttonSaveVelocity.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSaveVelocity.UseVisualStyleBackColor = true;
            buttonSaveVelocity.Click += buttonSaveVelocity_Click;
            // 
            // buttonCloseVelocity
            // 
            buttonCloseVelocity.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonCloseVelocity.Image = (Image)resources.GetObject("buttonCloseVelocity.Image");
            buttonCloseVelocity.Location = new Point(410, 10);
            buttonCloseVelocity.Name = "buttonCloseVelocity";
            buttonCloseVelocity.Size = new Size(80, 30);
            buttonCloseVelocity.TabIndex = 1;
            buttonCloseVelocity.Text = "Close";
            buttonCloseVelocity.TextAlign = ContentAlignment.MiddleLeft;
            buttonCloseVelocity.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCloseVelocity.UseVisualStyleBackColor = true;
            buttonCloseVelocity.Click += buttonCloseVelocity_Click;
            // 
            // panel10
            // 
            panel10.BackColor = Color.DarkSalmon;
            panel10.Dock = DockStyle.Bottom;
            panel10.Location = new Point(4, 466);
            panel10.Name = "panel10";
            panel10.Size = new Size(512, 4);
            panel10.TabIndex = 33;
            // 
            // panel11
            // 
            panel11.BackColor = Color.DarkSalmon;
            panel11.Dock = DockStyle.Right;
            panel11.Location = new Point(516, 4);
            panel11.Name = "panel11";
            panel11.Size = new Size(4, 466);
            panel11.TabIndex = 33;
            // 
            // panel12
            // 
            panel12.BackColor = Color.DarkSalmon;
            panel12.Dock = DockStyle.Top;
            panel12.Location = new Point(4, 0);
            panel12.Name = "panel12";
            panel12.Size = new Size(516, 4);
            panel12.TabIndex = 32;
            // 
            // panel13
            // 
            panel13.BackColor = Color.DarkSalmon;
            panel13.Dock = DockStyle.Left;
            panel13.Location = new Point(0, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(4, 470);
            panel13.TabIndex = 31;
            // 
            // pictureBoxSatelliteVelocityTrajectoryViewer
            // 
            pictureBoxSatelliteVelocityTrajectoryViewer.BackColor = Color.White;
            pictureBoxSatelliteVelocityTrajectoryViewer.Location = new Point(10, 70);
            pictureBoxSatelliteVelocityTrajectoryViewer.Name = "pictureBoxSatelliteVelocityTrajectoryViewer";
            pictureBoxSatelliteVelocityTrajectoryViewer.Size = new Size(500, 400);
            pictureBoxSatelliteVelocityTrajectoryViewer.TabIndex = 29;
            pictureBoxSatelliteVelocityTrajectoryViewer.TabStop = false;
            pictureBoxSatelliteVelocityTrajectoryViewer.Paint += drawing_pictureBoxSatelliteVelocityTrajectoryViewer;
            // 
            // textBoxSatelliteName
            // 
            textBoxSatelliteName.Location = new Point(122, 16);
            textBoxSatelliteName.Name = "textBoxSatelliteName";
            textBoxSatelliteName.ReadOnly = true;
            textBoxSatelliteName.Size = new Size(100, 23);
            textBoxSatelliteName.TabIndex = 26;
            // 
            // labelSatelliteName
            // 
            labelSatelliteName.AutoSize = true;
            labelSatelliteName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelSatelliteName.Location = new Point(30, 20);
            labelSatelliteName.Name = "labelSatelliteName";
            labelSatelliteName.Size = new Size(92, 15);
            labelSatelliteName.TabIndex = 25;
            labelSatelliteName.Text = "Satellite Name:";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(buttonGetSatelliteReport);
            panel1.Controls.Add(textBoxVelocity);
            panel1.Controls.Add(textBoxPhiAngle);
            panel1.Controls.Add(radioButtonSS);
            panel1.Controls.Add(textBoxMass);
            panel1.Controls.Add(buttonSaveToFileSatellite);
            panel1.Controls.Add(buttonOpenFromFileSatellite);
            panel1.Controls.Add(radioButtonRS);
            panel1.Controls.Add(buttonUpdateSatellite);
            panel1.Controls.Add(textBoxThetaAngle);
            panel1.Controls.Add(textBoxOrbitRadius);
            panel1.Controls.Add(buttonCreateSatellite);
            panel1.Controls.Add(labelOrbitRadius);
            panel1.Controls.Add(labelThetaAngle);
            panel1.Controls.Add(labelVelocity);
            panel1.Controls.Add(labelMass);
            panel1.Controls.Add(labelFiAngle);
            panel1.Controls.Add(labelSatelliteType);
            panel1.Location = new Point(550, 40);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 530);
            panel1.TabIndex = 26;
            // 
            // buttonGetSatelliteReport
            // 
            buttonGetSatelliteReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonGetSatelliteReport.Image = (Image)resources.GetObject("buttonGetSatelliteReport.Image");
            buttonGetSatelliteReport.ImageAlign = ContentAlignment.MiddleLeft;
            buttonGetSatelliteReport.Location = new Point(25, 490);
            buttonGetSatelliteReport.Name = "buttonGetSatelliteReport";
            buttonGetSatelliteReport.Size = new Size(114, 30);
            buttonGetSatelliteReport.TabIndex = 35;
            buttonGetSatelliteReport.Text = "Open Report";
            buttonGetSatelliteReport.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonGetSatelliteReport.UseVisualStyleBackColor = true;
            buttonGetSatelliteReport.Click += buttonGetSatelliteReport_Click;
            // 
            // radioButtonSS
            // 
            radioButtonSS.AutoSize = true;
            radioButtonSS.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radioButtonSS.Location = new Point(25, 55);
            radioButtonSS.Name = "radioButtonSS";
            radioButtonSS.Size = new Size(114, 19);
            radioButtonSS.TabIndex = 24;
            radioButtonSS.TabStop = true;
            radioButtonSS.Text = "Sender Satellite";
            radioButtonSS.UseVisualStyleBackColor = true;
            // 
            // buttonSaveToFileSatellite
            // 
            buttonSaveToFileSatellite.BackColor = Color.FromArgb(224, 224, 224);
            buttonSaveToFileSatellite.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonSaveToFileSatellite.Image = (Image)resources.GetObject("buttonSaveToFileSatellite.Image");
            buttonSaveToFileSatellite.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSaveToFileSatellite.Location = new Point(25, 450);
            buttonSaveToFileSatellite.Name = "buttonSaveToFileSatellite";
            buttonSaveToFileSatellite.Size = new Size(113, 30);
            buttonSaveToFileSatellite.TabIndex = 27;
            buttonSaveToFileSatellite.Text = "Save To File";
            buttonSaveToFileSatellite.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSaveToFileSatellite.UseVisualStyleBackColor = false;
            buttonSaveToFileSatellite.Click += buttonSaveToFileSatellite_Click;
            // 
            // buttonOpenFromFileSatellite
            // 
            buttonOpenFromFileSatellite.BackColor = Color.FromArgb(224, 224, 224);
            buttonOpenFromFileSatellite.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonOpenFromFileSatellite.Image = (Image)resources.GetObject("buttonOpenFromFileSatellite.Image");
            buttonOpenFromFileSatellite.ImageAlign = ContentAlignment.MiddleLeft;
            buttonOpenFromFileSatellite.Location = new Point(25, 450);
            buttonOpenFromFileSatellite.Name = "buttonOpenFromFileSatellite";
            buttonOpenFromFileSatellite.Size = new Size(123, 30);
            buttonOpenFromFileSatellite.TabIndex = 28;
            buttonOpenFromFileSatellite.Text = "Open From File";
            buttonOpenFromFileSatellite.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonOpenFromFileSatellite.UseVisualStyleBackColor = false;
            buttonOpenFromFileSatellite.Click += buttonOpenFromFileSatellite_Click;
            // 
            // radioButtonRS
            // 
            radioButtonRS.AutoSize = true;
            radioButtonRS.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radioButtonRS.Location = new Point(25, 30);
            radioButtonRS.Name = "radioButtonRS";
            radioButtonRS.Size = new Size(124, 19);
            radioButtonRS.TabIndex = 23;
            radioButtonRS.TabStop = true;
            radioButtonRS.Text = "Receiver Satellite";
            radioButtonRS.UseVisualStyleBackColor = true;
            // 
            // buttonUpdateSatellite
            // 
            buttonUpdateSatellite.BackColor = Color.FromArgb(224, 224, 224);
            buttonUpdateSatellite.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonUpdateSatellite.Image = (Image)resources.GetObject("buttonUpdateSatellite.Image");
            buttonUpdateSatellite.ImageAlign = ContentAlignment.MiddleLeft;
            buttonUpdateSatellite.Location = new Point(25, 410);
            buttonUpdateSatellite.Name = "buttonUpdateSatellite";
            buttonUpdateSatellite.Size = new Size(90, 30);
            buttonUpdateSatellite.TabIndex = 17;
            buttonUpdateSatellite.Text = "Update";
            buttonUpdateSatellite.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonUpdateSatellite.UseVisualStyleBackColor = false;
            buttonUpdateSatellite.Click += buttonUpdateSatellite_Click;
            // 
            // buttonCreateSatellite
            // 
            buttonCreateSatellite.BackColor = Color.FromArgb(224, 224, 224);
            buttonCreateSatellite.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonCreateSatellite.Image = (Image)resources.GetObject("buttonCreateSatellite.Image");
            buttonCreateSatellite.ImageAlign = ContentAlignment.MiddleLeft;
            buttonCreateSatellite.Location = new Point(25, 410);
            buttonCreateSatellite.Name = "buttonCreateSatellite";
            buttonCreateSatellite.Size = new Size(100, 30);
            buttonCreateSatellite.TabIndex = 25;
            buttonCreateSatellite.Text = "Add";
            buttonCreateSatellite.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCreateSatellite.UseVisualStyleBackColor = false;
            buttonCreateSatellite.Click += buttonCreateSatellite_Click;
            // 
            // labelOrbitRadius
            // 
            labelOrbitRadius.AutoSize = true;
            labelOrbitRadius.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelOrbitRadius.Location = new Point(23, 90);
            labelOrbitRadius.Name = "labelOrbitRadius";
            labelOrbitRadius.Size = new Size(135, 20);
            labelOrbitRadius.TabIndex = 6;
            labelOrbitRadius.Text = "Satellite Orbit (m)";
            // 
            // labelThetaAngle
            // 
            labelThetaAngle.AutoSize = true;
            labelThetaAngle.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelThetaAngle.Location = new Point(23, 150);
            labelThetaAngle.Name = "labelThetaAngle";
            labelThetaAngle.Size = new Size(94, 20);
            labelThetaAngle.TabIndex = 12;
            labelThetaAngle.Text = "Theta Angle";
            // 
            // labelVelocity
            // 
            labelVelocity.AutoSize = true;
            labelVelocity.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelVelocity.Location = new Point(23, 345);
            labelVelocity.Name = "labelVelocity";
            labelVelocity.Size = new Size(108, 20);
            labelVelocity.TabIndex = 13;
            labelVelocity.Text = "Velocity (m/s)";
            // 
            // labelMass
            // 
            labelMass.AutoSize = true;
            labelMass.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelMass.Location = new Point(23, 280);
            labelMass.Name = "labelMass";
            labelMass.Size = new Size(78, 20);
            labelMass.TabIndex = 14;
            labelMass.Text = "Mass (kg)";
            // 
            // labelFiAngle
            // 
            labelFiAngle.AutoSize = true;
            labelFiAngle.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelFiAngle.Location = new Point(23, 215);
            labelFiAngle.Name = "labelFiAngle";
            labelFiAngle.Size = new Size(76, 20);
            labelFiAngle.TabIndex = 15;
            labelFiAngle.Text = "Phi Angle";
            // 
            // labelSatelliteType
            // 
            labelSatelliteType.AutoSize = true;
            labelSatelliteType.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelSatelliteType.Location = new Point(20, 5);
            labelSatelliteType.Name = "labelSatelliteType";
            labelSatelliteType.Size = new Size(102, 20);
            labelSatelliteType.TabIndex = 16;
            labelSatelliteType.Text = "Satellite Type";
            // 
            // pictureBoxSatellite
            // 
            pictureBoxSatellite.BackgroundImage = (Image)resources.GetObject("pictureBoxSatellite.BackgroundImage");
            pictureBoxSatellite.Image = (Image)resources.GetObject("pictureBoxSatellite.Image");
            pictureBoxSatellite.Location = new Point(420, 229);
            pictureBoxSatellite.Name = "pictureBoxSatellite";
            pictureBoxSatellite.Size = new Size(15, 15);
            pictureBoxSatellite.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxSatellite.TabIndex = 9;
            pictureBoxSatellite.TabStop = false;
            // 
            // pictureBoxWorld
            // 
            pictureBoxWorld.BackgroundImage = (Image)resources.GetObject("pictureBoxWorld.BackgroundImage");
            pictureBoxWorld.Image = (Image)resources.GetObject("pictureBoxWorld.Image");
            pictureBoxWorld.Location = new Point(209, 229);
            pictureBoxWorld.Name = "pictureBoxWorld";
            pictureBoxWorld.Size = new Size(100, 100);
            pictureBoxWorld.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxWorld.TabIndex = 8;
            pictureBoxWorld.TabStop = false;
            // 
            // pictureBoxSatelliteViewer
            // 
            pictureBoxSatelliteViewer.BackColor = SystemColors.Window;
            pictureBoxSatelliteViewer.BackgroundImage = (Image)resources.GetObject("pictureBoxSatelliteViewer.BackgroundImage");
            pictureBoxSatelliteViewer.Location = new Point(30, 40);
            pictureBoxSatelliteViewer.Name = "pictureBoxSatelliteViewer";
            pictureBoxSatelliteViewer.Size = new Size(500, 500);
            pictureBoxSatelliteViewer.TabIndex = 7;
            pictureBoxSatelliteViewer.TabStop = false;
            pictureBoxSatelliteViewer.Paint += drawing_PictureBoxSatelliteViewer;
            // 
            // buttonStartSimulation
            // 
            buttonStartSimulation.BackColor = Color.FromArgb(224, 224, 224);
            buttonStartSimulation.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonStartSimulation.Image = (Image)resources.GetObject("buttonStartSimulation.Image");
            buttonStartSimulation.ImageAlign = ContentAlignment.MiddleLeft;
            buttonStartSimulation.Location = new Point(700, 25);
            buttonStartSimulation.Name = "buttonStartSimulation";
            buttonStartSimulation.Size = new Size(70, 30);
            buttonStartSimulation.TabIndex = 22;
            buttonStartSimulation.Text = "Start";
            buttonStartSimulation.TextAlign = ContentAlignment.MiddleLeft;
            buttonStartSimulation.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonStartSimulation.UseVisualStyleBackColor = false;
            buttonStartSimulation.Click += buttonStartSimulation_Click;
            // 
            // labelTimeStep
            // 
            labelTimeStep.AutoSize = true;
            labelTimeStep.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelTimeStep.Location = new Point(570, 10);
            labelTimeStep.Name = "labelTimeStep";
            labelTimeStep.Size = new Size(79, 20);
            labelTimeStep.TabIndex = 21;
            labelTimeStep.Text = "Time Step";
            // 
            // labelSimulationTime
            // 
            labelSimulationTime.AutoSize = true;
            labelSimulationTime.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelSimulationTime.Location = new Point(418, 10);
            labelSimulationTime.Name = "labelSimulationTime";
            labelSimulationTime.Size = new Size(123, 20);
            labelSimulationTime.TabIndex = 20;
            labelSimulationTime.Text = "Simulation Time";
            // 
            // textBoxSimulationTime
            // 
            textBoxSimulationTime.Location = new Point(420, 32);
            textBoxSimulationTime.Name = "textBoxSimulationTime";
            textBoxSimulationTime.Size = new Size(123, 23);
            textBoxSimulationTime.TabIndex = 18;
            // 
            // textBoxTimeStep
            // 
            textBoxTimeStep.Location = new Point(575, 32);
            textBoxTimeStep.Name = "textBoxTimeStep";
            textBoxTimeStep.Size = new Size(100, 23);
            textBoxTimeStep.TabIndex = 19;
            // 
            // panelGroundStation
            // 
            panelGroundStation.BackColor = SystemColors.Window;
            panelGroundStation.BorderStyle = BorderStyle.FixedSingle;
            panelGroundStation.Controls.Add(labelGroundStationName);
            panelGroundStation.Controls.Add(textBoxGroundStationName);
            panelGroundStation.Controls.Add(pictureBoxGroundStation);
            panelGroundStation.Controls.Add(pictureBoxWorld_2);
            panelGroundStation.Controls.Add(pictureBoxGroundStationViewer);
            panelGroundStation.Controls.Add(panel3);
            panelGroundStation.Location = new Point(200, 80);
            panelGroundStation.Name = "panelGroundStation";
            panelGroundStation.Size = new Size(770, 585);
            panelGroundStation.TabIndex = 11;
            // 
            // labelGroundStationName
            // 
            labelGroundStationName.AutoSize = true;
            labelGroundStationName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelGroundStationName.Location = new Point(30, 20);
            labelGroundStationName.Name = "labelGroundStationName";
            labelGroundStationName.Size = new Size(131, 15);
            labelGroundStationName.TabIndex = 36;
            labelGroundStationName.Text = "Ground Station Name:";
            // 
            // textBoxGroundStationName
            // 
            textBoxGroundStationName.Location = new Point(162, 16);
            textBoxGroundStationName.Name = "textBoxGroundStationName";
            textBoxGroundStationName.ReadOnly = true;
            textBoxGroundStationName.Size = new Size(100, 23);
            textBoxGroundStationName.TabIndex = 35;
            // 
            // pictureBoxGroundStation
            // 
            pictureBoxGroundStation.BackgroundImage = (Image)resources.GetObject("pictureBoxGroundStation.BackgroundImage");
            pictureBoxGroundStation.Image = (Image)resources.GetObject("pictureBoxGroundStation.Image");
            pictureBoxGroundStation.Location = new Point(372, 256);
            pictureBoxGroundStation.Name = "pictureBoxGroundStation";
            pictureBoxGroundStation.Size = new Size(40, 50);
            pictureBoxGroundStation.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxGroundStation.TabIndex = 31;
            pictureBoxGroundStation.TabStop = false;
            // 
            // pictureBoxWorld_2
            // 
            pictureBoxWorld_2.BackgroundImage = (Image)resources.GetObject("pictureBoxWorld_2.BackgroundImage");
            pictureBoxWorld_2.Image = (Image)resources.GetObject("pictureBoxWorld_2.Image");
            pictureBoxWorld_2.Location = new Point(98, 102);
            pictureBoxWorld_2.Name = "pictureBoxWorld_2";
            pictureBoxWorld_2.Size = new Size(350, 350);
            pictureBoxWorld_2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxWorld_2.TabIndex = 30;
            pictureBoxWorld_2.TabStop = false;
            pictureBoxWorld_2.Paint += drawing_PictureBoxWorld_2;
            // 
            // pictureBoxGroundStationViewer
            // 
            pictureBoxGroundStationViewer.Image = (Image)resources.GetObject("pictureBoxGroundStationViewer.Image");
            pictureBoxGroundStationViewer.Location = new Point(30, 40);
            pictureBoxGroundStationViewer.Name = "pictureBoxGroundStationViewer";
            pictureBoxGroundStationViewer.Size = new Size(500, 500);
            pictureBoxGroundStationViewer.TabIndex = 29;
            pictureBoxGroundStationViewer.TabStop = false;
            pictureBoxGroundStationViewer.Paint += drawing_PictureBoxGroundStationViewer;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(buttonGetGroundStationReport);
            panel3.Controls.Add(radioButtonBCT_GroundStation);
            panel3.Controls.Add(radioButtonT_GroundStation);
            panel3.Controls.Add(buttonSaveToFileGS);
            panel3.Controls.Add(buttonOpenFromFileGS);
            panel3.Controls.Add(buttonUpdateGroundStation);
            panel3.Controls.Add(radioButtonC_GroundStation);
            panel3.Controls.Add(textBoxGroundStationFoVAngle);
            panel3.Controls.Add(textBoxGroundStationTheta);
            panel3.Controls.Add(labelGroundStationTheta);
            panel3.Controls.Add(labelGroundStationFoVAngle);
            panel3.Controls.Add(labelGroundStationType);
            panel3.Controls.Add(buttonCreateGroundStation);
            panel3.Location = new Point(550, 40);
            panel3.Name = "panel3";
            panel3.Size = new Size(212, 420);
            panel3.TabIndex = 27;
            // 
            // buttonGetGroundStationReport
            // 
            buttonGetGroundStationReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonGetGroundStationReport.Image = (Image)resources.GetObject("buttonGetGroundStationReport.Image");
            buttonGetGroundStationReport.ImageAlign = ContentAlignment.MiddleLeft;
            buttonGetGroundStationReport.Location = new Point(25, 360);
            buttonGetGroundStationReport.Name = "buttonGetGroundStationReport";
            buttonGetGroundStationReport.Size = new Size(114, 30);
            buttonGetGroundStationReport.TabIndex = 36;
            buttonGetGroundStationReport.Text = "Open Report";
            buttonGetGroundStationReport.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonGetGroundStationReport.UseVisualStyleBackColor = true;
            buttonGetGroundStationReport.Click += buttonGetGroundStationReport_Click;
            // 
            // radioButtonBCT_GroundStation
            // 
            radioButtonBCT_GroundStation.AutoSize = true;
            radioButtonBCT_GroundStation.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            radioButtonBCT_GroundStation.Location = new Point(25, 100);
            radioButtonBCT_GroundStation.Name = "radioButtonBCT_GroundStation";
            radioButtonBCT_GroundStation.Size = new Size(188, 19);
            radioButtonBCT_GroundStation.TabIndex = 25;
            radioButtonBCT_GroundStation.TabStop = true;
            radioButtonBCT_GroundStation.Text = "Both Communicate and Track";
            radioButtonBCT_GroundStation.UseVisualStyleBackColor = true;
            // 
            // radioButtonT_GroundStation
            // 
            radioButtonT_GroundStation.AutoSize = true;
            radioButtonT_GroundStation.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            radioButtonT_GroundStation.Location = new Point(25, 75);
            radioButtonT_GroundStation.Name = "radioButtonT_GroundStation";
            radioButtonT_GroundStation.Size = new Size(83, 19);
            radioButtonT_GroundStation.TabIndex = 24;
            radioButtonT_GroundStation.TabStop = true;
            radioButtonT_GroundStation.Text = "Only Track";
            radioButtonT_GroundStation.UseVisualStyleBackColor = true;
            // 
            // buttonSaveToFileGS
            // 
            buttonSaveToFileGS.BackColor = Color.FromArgb(224, 224, 224);
            buttonSaveToFileGS.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonSaveToFileGS.Image = (Image)resources.GetObject("buttonSaveToFileGS.Image");
            buttonSaveToFileGS.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSaveToFileGS.Location = new Point(25, 320);
            buttonSaveToFileGS.Name = "buttonSaveToFileGS";
            buttonSaveToFileGS.Size = new Size(113, 30);
            buttonSaveToFileGS.TabIndex = 33;
            buttonSaveToFileGS.Text = "Save to file";
            buttonSaveToFileGS.TextAlign = ContentAlignment.MiddleLeft;
            buttonSaveToFileGS.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSaveToFileGS.UseVisualStyleBackColor = false;
            buttonSaveToFileGS.Click += buttonSaveToFileGS_Click;
            // 
            // buttonOpenFromFileGS
            // 
            buttonOpenFromFileGS.BackColor = Color.FromArgb(224, 224, 224);
            buttonOpenFromFileGS.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonOpenFromFileGS.Image = (Image)resources.GetObject("buttonOpenFromFileGS.Image");
            buttonOpenFromFileGS.Location = new Point(25, 320);
            buttonOpenFromFileGS.Name = "buttonOpenFromFileGS";
            buttonOpenFromFileGS.Size = new Size(123, 30);
            buttonOpenFromFileGS.TabIndex = 34;
            buttonOpenFromFileGS.Text = "Open from file";
            buttonOpenFromFileGS.TextAlign = ContentAlignment.MiddleLeft;
            buttonOpenFromFileGS.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonOpenFromFileGS.UseVisualStyleBackColor = false;
            buttonOpenFromFileGS.Click += buttonOpenFromFileGS_Click;
            // 
            // buttonUpdateGroundStation
            // 
            buttonUpdateGroundStation.BackColor = Color.FromArgb(224, 224, 224);
            buttonUpdateGroundStation.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonUpdateGroundStation.Image = (Image)resources.GetObject("buttonUpdateGroundStation.Image");
            buttonUpdateGroundStation.Location = new Point(25, 280);
            buttonUpdateGroundStation.Name = "buttonUpdateGroundStation";
            buttonUpdateGroundStation.Size = new Size(90, 30);
            buttonUpdateGroundStation.TabIndex = 32;
            buttonUpdateGroundStation.Text = "Update";
            buttonUpdateGroundStation.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonUpdateGroundStation.UseVisualStyleBackColor = false;
            buttonUpdateGroundStation.Click += buttonUpdateGroundStation_Click;
            // 
            // radioButtonC_GroundStation
            // 
            radioButtonC_GroundStation.AutoSize = true;
            radioButtonC_GroundStation.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            radioButtonC_GroundStation.Location = new Point(25, 50);
            radioButtonC_GroundStation.Name = "radioButtonC_GroundStation";
            radioButtonC_GroundStation.Size = new Size(130, 19);
            radioButtonC_GroundStation.TabIndex = 23;
            radioButtonC_GroundStation.TabStop = true;
            radioButtonC_GroundStation.Text = "Only Communicate";
            radioButtonC_GroundStation.UseVisualStyleBackColor = true;
            // 
            // textBoxGroundStationFoVAngle
            // 
            textBoxGroundStationFoVAngle.Location = new Point(25, 230);
            textBoxGroundStationFoVAngle.Name = "textBoxGroundStationFoVAngle";
            textBoxGroundStationFoVAngle.Size = new Size(100, 23);
            textBoxGroundStationFoVAngle.TabIndex = 1;
            textBoxGroundStationFoVAngle.TextChanged += textBoxGroundStationFoVAngle_TextChanged;
            // 
            // textBoxGroundStationTheta
            // 
            textBoxGroundStationTheta.Location = new Point(25, 165);
            textBoxGroundStationTheta.Name = "textBoxGroundStationTheta";
            textBoxGroundStationTheta.Size = new Size(100, 23);
            textBoxGroundStationTheta.TabIndex = 0;
            textBoxGroundStationTheta.TextChanged += textBoxGroundStationTheta_TextChanged;
            // 
            // labelGroundStationTheta
            // 
            labelGroundStationTheta.AutoSize = true;
            labelGroundStationTheta.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelGroundStationTheta.Location = new Point(23, 140);
            labelGroundStationTheta.Name = "labelGroundStationTheta";
            labelGroundStationTheta.Size = new Size(94, 20);
            labelGroundStationTheta.TabIndex = 6;
            labelGroundStationTheta.Text = "Theta Angle";
            // 
            // labelGroundStationFoVAngle
            // 
            labelGroundStationFoVAngle.AutoSize = true;
            labelGroundStationFoVAngle.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelGroundStationFoVAngle.Location = new Point(23, 205);
            labelGroundStationFoVAngle.Name = "labelGroundStationFoVAngle";
            labelGroundStationFoVAngle.Size = new Size(144, 20);
            labelGroundStationFoVAngle.TabIndex = 12;
            labelGroundStationFoVAngle.Text = "Field of View Angle";
            // 
            // labelGroundStationType
            // 
            labelGroundStationType.AutoSize = true;
            labelGroundStationType.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelGroundStationType.ForeColor = Color.Black;
            labelGroundStationType.Location = new Point(20, 20);
            labelGroundStationType.Name = "labelGroundStationType";
            labelGroundStationType.Size = new Size(153, 20);
            labelGroundStationType.TabIndex = 16;
            labelGroundStationType.Text = "Ground Station Type";
            // 
            // buttonCreateGroundStation
            // 
            buttonCreateGroundStation.BackColor = Color.FromArgb(224, 224, 224);
            buttonCreateGroundStation.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonCreateGroundStation.ForeColor = SystemColors.ControlText;
            buttonCreateGroundStation.Image = (Image)resources.GetObject("buttonCreateGroundStation.Image");
            buttonCreateGroundStation.ImageAlign = ContentAlignment.MiddleLeft;
            buttonCreateGroundStation.Location = new Point(25, 280);
            buttonCreateGroundStation.Name = "buttonCreateGroundStation";
            buttonCreateGroundStation.Size = new Size(100, 30);
            buttonCreateGroundStation.TabIndex = 28;
            buttonCreateGroundStation.Text = "Add";
            buttonCreateGroundStation.TextAlign = ContentAlignment.MiddleLeft;
            buttonCreateGroundStation.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCreateGroundStation.UseVisualStyleBackColor = false;
            buttonCreateGroundStation.Click += buttonCreateGroundStation_Click;
            // 
            // progressBarSimulation
            // 
            progressBarSimulation.Location = new Point(777, 25);
            progressBarSimulation.Name = "progressBarSimulation";
            progressBarSimulation.Size = new Size(175, 30);
            progressBarSimulation.TabIndex = 10;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(radioButtonEuler);
            panel2.Controls.Add(radioButtonRungeKutta);
            panel2.Controls.Add(labelSimulationType);
            panel2.Controls.Add(labelProgressBar);
            panel2.Controls.Add(progressBarSimulation);
            panel2.Controls.Add(labelSimulationTime);
            panel2.Controls.Add(textBoxSimulationTime);
            panel2.Controls.Add(labelTimeStep);
            panel2.Controls.Add(textBoxTimeStep);
            panel2.Controls.Add(buttonStartSimulation);
            panel2.Location = new Point(10, 10);
            panel2.Name = "panel2";
            panel2.Size = new Size(960, 62);
            panel2.TabIndex = 12;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(15, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(120, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 27;
            pictureBox1.TabStop = false;
            // 
            // radioButtonEuler
            // 
            radioButtonEuler.AutoSize = true;
            radioButtonEuler.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            radioButtonEuler.Location = new Point(170, 32);
            radioButtonEuler.Name = "radioButtonEuler";
            radioButtonEuler.Size = new Size(53, 19);
            radioButtonEuler.TabIndex = 26;
            radioButtonEuler.TabStop = true;
            radioButtonEuler.Text = "Euler";
            radioButtonEuler.UseVisualStyleBackColor = true;
            // 
            // radioButtonRungeKutta
            // 
            radioButtonRungeKutta.AutoSize = true;
            radioButtonRungeKutta.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            radioButtonRungeKutta.Location = new Point(230, 32);
            radioButtonRungeKutta.Name = "radioButtonRungeKutta";
            radioButtonRungeKutta.Size = new Size(155, 19);
            radioButtonRungeKutta.TabIndex = 25;
            radioButtonRungeKutta.TabStop = true;
            radioButtonRungeKutta.Text = "Runge-Kutta 4th Order";
            radioButtonRungeKutta.UseVisualStyleBackColor = true;
            // 
            // labelSimulationType
            // 
            labelSimulationType.AutoSize = true;
            labelSimulationType.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelSimulationType.Location = new Point(164, 10);
            labelSimulationType.Name = "labelSimulationType";
            labelSimulationType.Size = new Size(147, 20);
            labelSimulationType.TabIndex = 24;
            labelSimulationType.Text = "Integration Method";
            // 
            // labelProgressBar
            // 
            labelProgressBar.AutoSize = true;
            labelProgressBar.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelProgressBar.Location = new Point(775, 2);
            labelProgressBar.Name = "labelProgressBar";
            labelProgressBar.Size = new Size(149, 20);
            labelProgressBar.TabIndex = 23;
            labelProgressBar.Text = "Simulation Progress";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 676);
            Controls.Add(panel2);
            Controls.Add(panelMainPage);
            Controls.Add(panelSatellite);
            Controls.Add(panelGroundStation);
            Controls.Add(treeViewSpaceSystem);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Space System Simulation";
            Load += MainForm_Load;
            panelSatellite.ResumeLayout(false);
            panelSatellite.PerformLayout();
            panelSatellitePositionTrajectoryViewer.ResumeLayout(false);
            panelSatellitePositionTrajectoryController.ResumeLayout(false);
            panelSatellitePositionTrajectoryController.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSatelliteTrajectoryViewer).EndInit();
            panelMainPage.ResumeLayout(false);
            panelMainPage.PerformLayout();
            panelSatelliteVelocityTrajectoryViewer.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSatelliteVelocityTrajectoryViewer).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSatellite).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWorld).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSatelliteViewer).EndInit();
            panelGroundStation.ResumeLayout(false);
            panelGroundStation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGroundStation).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWorld_2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGroundStationViewer).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxOrbitRadius;
        private TextBox textBoxThetaAngle;
        private TextBox textBoxPhiAngle;
        private TextBox textBoxMass;
        private TextBox textBoxVelocity;
        private TreeView treeViewSpaceSystem;
        private Panel panelSatellite;
        private Label labelOrbitRadius;
        private PictureBox pictureBoxSatellite;
        private PictureBox pictureBoxWorld;
        private PictureBox pictureBoxSatelliteViewer;
        private Label labelThetaAngle;
        private Label labelSatelliteType;
        private Label labelFiAngle;
        private Label labelMass;
        private Label labelVelocity;
        private Button buttonUpdateSatellite;
        private Label labelTimeStep;
        private Label labelSimulationTime;
        private TextBox textBoxSimulationTime;
        private TextBox textBoxTimeStep;
        private Button buttonStartSimulation;
        private RadioButton radioButtonSS;
        private RadioButton radioButtonRS;
        private Button buttonCreateSatellite;
        private Panel panel1;
        private Button buttonSaveToFileSatellite;
        public ProgressBar progressBarSimulation;
        private Button buttonOpenFromFileSatellite;
        private Label labelSatelliteName;
        private TextBox textBoxSatelliteName;
        private Panel panelGroundStation;
        private Panel panel3;
        private RadioButton radioButtonBCT_GroundStation;
        private RadioButton radioButtonT_GroundStation;
        private RadioButton radioButtonC_GroundStation;
        private TextBox textBoxGroundStationFoVAngle;
        private TextBox textBoxGroundStationTheta;
        private Label labelGroundStationTheta;
        private Label labelGroundStationFoVAngle;
        private Label labelGroundStationType;
        private Button buttonCreateGroundStation;
        private PictureBox pictureBoxGroundStationViewer;
        private PictureBox pictureBoxWorld_2;
        private PictureBox pictureBoxGroundStation;
        private Button buttonUpdateGroundStation;
        private Button buttonSaveToFileGS;
        private Button buttonOpenFromFileGS;
        private TextBox textBoxGroundStationName;
        private Label labelGroundStationName;
        private Panel panel2;
        private Label labelProgressBar;
        private PictureBox pictureBoxSatelliteTrajectoryViewer;
        private Panel panelSatellitePositionTrajectoryViewer;
        private Panel panelSatellitePositionTrajectoryController;
        private Button buttonClosePosition;
        private Panel panel7;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Button buttonSavePosition;
        private Button buttonShowPositionGraph;
        private Panel panelSatelliteVelocityTrajectoryViewer;
        private Panel panel9;
        private Button buttonSaveVelocity;
        private Button buttonCloseVelocity;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private Panel panel13;
        private PictureBox pictureBoxSatelliteVelocityTrajectoryViewer;
        private Button buttonShowVelocityGraph;
        private ImageList ımageList1;
        private Label labelVelocityGraph;
        private Label labelPositionGraph;
        private Label labelSimulationType;
        public RadioButton radioButtonEuler;
        public RadioButton radioButtonRungeKutta;
        private PictureBox pictureBox1;
        private Panel panelMainPage;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label7;
        private Label label6;
        private Button buttonGetSatelliteReport;
        private Button buttonGetGroundStationReport;
    }
}
