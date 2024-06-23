using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using TextBox = System.Windows.Forms.TextBox;
using System.Diagnostics;

namespace Space_Simulation
{

    /* 
        Code written by Hamdi EKIZ. 
        This code written as a part of term project of the course "Advanced Programming" spring of 2024, 
        Departments of Aerospace and Mechanical Engineering at Gebze Technical University.
        Course Instructor : Seyed Yaser Nabavi Chashmi
        Course Title : Advanced Programming (ME-108 and AERO-108)
        Date : 01 June 2024
    */

    public partial class MainForm : Form
    {
        private DrawSatellite drawSatellite;
        private DrawGroundStation drawGroundStation;
        private DrawTrajectoryPosition drawTrajectory;
        private DrawTrajectoryVelocity drawTrajectoryVelocity;
        private SatelliteType satelliteType;
        private GroundStationType groundStationType;
        private Simulation simulation;

        // Store the original sizes and pixelScale
        private Size originalWorldSize;
        private Size originalSatelliteSize;
        private double originalPixelScale;

        // Dictionary to store satellite positions
        public Dictionary<Satellite, List<(double X, double Y)>> Positions { get; set; }

        // Dictionary to store satellite velocity
        public Dictionary<Satellite, List<(double X, double Y)>> Velocities { get; set; }

        public MainForm()
        {
            InitializeComponent();
            this.MaximizeBox = false; // Do not allow user to change the scale of the form
        }

        // Region for the store parameters
        #region Parameters
        public double OrbitRadius { get; set; } // Orbit radius of the satellite
        public double SatelliteThetaAngle { get; set; } // Theta angle of the satellite
        public double PhiAngle { get; set; } // Phi angle of the satellite
        public double Mass { get; set; } // Mass of the satellite
        public double Velocity { get; set; } // Velocity of the satellite
        public double FinalTime { get; set; } // Simulation time 
        public double TimeStep { get; set; } // Time step of the simulation
        public double GroundStationThetaAngle { get; set; } // Theta angle of the ground station
        public double FieldOfViewAngle { get; set; } // Field of view angle of the ground station

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            drawSatellite = new DrawSatellite(pictureBoxSatelliteViewer, pictureBoxWorld, pictureBoxSatellite,
                                              textBoxThetaAngle, textBoxPhiAngle, textBoxOrbitRadius);
            drawGroundStation = new DrawGroundStation(pictureBoxGroundStationViewer, pictureBoxWorld_2, pictureBoxGroundStation,
                                                      textBoxGroundStationTheta, textBoxGroundStationFoVAngle);
            drawTrajectory = new DrawTrajectoryPosition(pictureBoxSatelliteTrajectoryViewer);
            drawTrajectoryVelocity = new DrawTrajectoryVelocity(pictureBoxSatelliteVelocityTrajectoryViewer);
            Positions = new Dictionary<Satellite, List<(double X, double Y)>>();
            Velocities = new Dictionary<Satellite, List<(double X, double Y)>>();
        }

        // Region for the store method for draw somethings
        #region Drawing Controllers

        // Method for drawing the ground station according to the values (theta angle, field of view angle) ​​entered by the user
        private void drawing_PictureBoxGroundStationViewer(object sender, PaintEventArgs e)
        {
            drawGroundStation.graphics = e.Graphics;
            drawGroundStation.Draw_CoordinateSystem();
            drawGroundStation.SetPictureBoxWorldPosition();
            drawGroundStation.SetPictureBoxGroundStationPosition();
            drawGroundStation.Draw_RadiusCon();
            drawGroundStation.Draw_FieldOfViewCon();
        }

        // Method for drawing the satellite according to the values (theta angle, phi angle, satellite orbit) ​​entered by the user
        private void drawing_PictureBoxSatelliteViewer(object sender, PaintEventArgs e)
        {
            drawSatellite.graphics = e.Graphics;
            drawSatellite.Draw_CoordinateSystem();
            drawSatellite.Draw_Radius();
            drawSatellite.Draw_Orbit();
            drawSatellite.Draw_Tangent();
            drawSatellite.Draw_VelocityLine();
            drawSatellite.SetPictureBoxWorldPosition();
            drawSatellite.SetPictureBoxSatellitePosition();
        }

        // Method for the rescaling World picture wrt written satellite orbit
        private void RescalePictureBasedOnSatelliteProperties(Satellite satellite)
        {
            drawSatellite.RescalePicture(
                satellite.Properties.InitialOrbitRadius,
                originalWorldSize,
                originalSatelliteSize,
                originalPixelScale
            );
        }

        // Method for drawing the world from the ground station representation
        // If this method does not exist, the drawing will look bad.
        private void drawing_PictureBoxWorld_2(object sender, PaintEventArgs e)
        {
            drawGroundStation.graphics = e.Graphics;
            drawGroundStation.Draw_Radius();
            drawGroundStation.Draw_FieldOfView();
        }

        // Method for drawing the trajectory of the each satellite position
        private void drawing_pictureBoxSatelliteTrajectoryViewer(object sender, PaintEventArgs e)
        {
            drawTrajectory.graphics = e.Graphics;
            drawTrajectory.Draw_CoordinateSystem();
            drawTrajectory.Draw_Border();
            drawTrajectory.Draw_PositionZero();
            drawPositionGraph();
            drawTrajectory.WriteGraphName();
        }

        // Method for drawing the trajectory of the each satellite velocity
        private void drawing_pictureBoxSatelliteVelocityTrajectoryViewer(object sender, PaintEventArgs e)
        {
            drawTrajectoryVelocity.graphics = e.Graphics;
            drawTrajectoryVelocity.Draw_CoordinateSystem();
            drawTrajectoryVelocity.Draw_Border();
            drawTrajectoryVelocity.Draw_PositionZero();
            drawVelocityGraph();
            drawTrajectoryVelocity.WriteGraphName();
        }

        // Method for drawing position (x,y) of the satellite
        // In this class there is a mistake check later
        public void drawPositionGraph()
        {
            if (treeViewSpaceSystem.SelectedNode != null && treeViewSpaceSystem.SelectedNode.Tag is Satellite satellite)
            {
                if (simulation.SatellitePositions.ContainsKey(satellite))
                {
                    Positions[satellite] = simulation.SatellitePositions[satellite];
                    drawTrajectory.Draw_Trajectory(Positions[satellite]);
                }
            }
        }


        // Method for drawing velocity (Vx, Vy) of the satellite
        // In this class there is a mistake check later 
        public void drawVelocityGraph()
        {
            if (treeViewSpaceSystem.SelectedNode != null && treeViewSpaceSystem.SelectedNode.Tag is Satellite satellite)
            {

                if (simulation.SatelliteVelocity.ContainsKey(satellite))
                {
                    var velocity = simulation.SatelliteVelocity[satellite];
                    drawTrajectoryVelocity.Draw_Trajectory(velocity);
                }
            }
        }

        #endregion

        // Region for the store method related to text boxes
        #region Text Box Controllers

        // Region for store method for text change events
        #region Text Changed

        private void textBoxThetaAngle_TextChanged(object sender, EventArgs e)
        {
            // If textBoxThetaAngle null or space character set as default value of 0
            if (string.IsNullOrWhiteSpace(textBoxThetaAngle.Text))
            {
                drawSatellite.ThetaAngle = 0;
            }
            // Redraw the coordinate system and refresh pictureBoxSatelliteViewer 
            drawSatellite.CoordinateSystemConverter(sender, e);
            pictureBoxSatelliteViewer.Refresh();
        }

        private void textBoxPhiAngle_TextChanged(object sender, EventArgs e)
        {
            // If textBoxPhiAngle null or space character set as default value of 0
            if (string.IsNullOrWhiteSpace(textBoxPhiAngle.Text))
            {
                drawSatellite.PhiAngle = 0;
            }
            // Redraw the coordinate system and refresh pictureBoxSatelliteViewer 
            drawSatellite.CoordinateSystemConverter(sender, e);
            pictureBoxSatelliteViewer.Refresh();
        }

        private void textBoxGroundStationTheta_TextChanged(object sender, EventArgs e)
        {
            // If textBoxGroundStationTheta null or space character set as default value of 0
            if (string.IsNullOrWhiteSpace(textBoxGroundStationTheta.Text))
            {
                drawGroundStation.GroundStationThetaAngle = 0;
            }
            // Redraw the coordinate system, refresh pictureBoxSatelliteViewer and pictureBoxWorld_2
            drawGroundStation.CoordinateSystemConverter(sender, e);
            pictureBoxGroundStationViewer.Refresh();
            pictureBoxWorld_2.Refresh();
        }

        private void textBoxGroundStationFoVAngle_TextChanged(object sender, EventArgs e)
        {
            // If textBoxGroundStationFoVAngle is null or space character, set default value of 0
            if (string.IsNullOrWhiteSpace(textBoxGroundStationFoVAngle.Text))
            {
                drawGroundStation.FieldOfViewAngle = 0;
            }
            else
            {
                // Try to parse the text to a double
                if (double.TryParse(textBoxGroundStationFoVAngle.Text, out double angle))
                {
                    // Check if the angle is greater than 180 degrees
                    if (angle > 180)
                    {
                        MessageBox.Show("Field of View angle cannot be greater than 180 degrees.", "Input Error", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxGroundStationFoVAngle.Text = "0"; // Reset to default value
                        drawGroundStation.FieldOfViewAngle = 0;
                    }
                    else
                    {
                        drawGroundStation.FieldOfViewAngle = angle;
                    }
                }
            }

            // Redraw the coordinate system, refresh pictureBoxGroundStationViewer and pictureBoxWorld_2
            drawGroundStation.CoordinateSystemConverter(sender, e);
            pictureBoxGroundStationViewer.Refresh();
            pictureBoxWorld_2.Refresh();
        }


        #endregion

        // Region for store method to display something on text boxes
        #region Text Box Displayer

        // This method works for when user choose a child node of satellite from the tree view field.
        // It helps to display the properties of related satellite.
        private void DisplayProperties(Satellite satellite)
        {
            // Display satellite properties
            textBoxOrbitRadius.Text = satellite.Properties.InitialOrbitRadius.ToString();
            textBoxThetaAngle.Text = satellite.Properties.InitialThetaAngle.ToString();
            textBoxPhiAngle.Text = satellite.Properties.InitialPhiAngle.ToString();
            textBoxMass.Text = satellite.Properties.SatelliteMass.ToString();
            textBoxVelocity.Text = satellite.Properties.SatelliteInitialVelocity.ToString();

            // Update radio button states based on satellite type
            if (satellite.Properties.SatelliteType == SatelliteType.ReceiverSatellite)
            {
                radioButtonRS.Checked = true;
                radioButtonSS.Checked = false;
            }
            else if (satellite.Properties.SatelliteType == SatelliteType.SenderSatellite)
            {
                radioButtonRS.Checked = false;
                radioButtonSS.Checked = true;
            }
        }

        // This method works for when user choose a child node of ground station from the tree view field.
        // It helps to display the properties of related ground station.
        private void DisplayProperties(GroundStation groundStation)
        {
            // Display satellite properties
            textBoxGroundStationTheta.Text = groundStation.ThetaAngle.ToString();
            textBoxGroundStationFoVAngle.Text = groundStation.FieldOfViewAngle.ToString();

            // Update radio button states based on satellite type
            if (groundStation.GroundStationName.Contains("T"))
            {
                radioButtonT_GroundStation.Checked = true;
                radioButtonC_GroundStation.Checked = false;
                radioButtonBCT_GroundStation.Checked = false;
            }
            else if (groundStation.GroundStationName.Contains("C"))
            {
                radioButtonT_GroundStation.Checked = false;
                radioButtonC_GroundStation.Checked = true;
                radioButtonBCT_GroundStation.Checked = false;
            }
            else if (groundStation.GroundStationName.Contains("B"))
            {
                radioButtonT_GroundStation.Checked = false;
                radioButtonC_GroundStation.Checked = false;
                radioButtonBCT_GroundStation.Checked = true;
            }
        }

        #endregion

        #endregion

        // Region for to store button click operations 
        #region Button Click Operations

        // Region for storing buttons for position graph
        #region Button Position Graph

        // When this button clicked show the user " panelSatellitePositionTrajectoryViewer "
        private void buttonShowPositionGraph_Click(object sender, EventArgs e)
        {
            // Check that is the simulation completed or not
            if (!simulationCompleted)
            {
                MessageBox.Show("Please simulate the program at least once before viewing the position graph.",
                                "Simulation Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            panelSatellitePositionTrajectoryViewer.Visible = true;
        }

        // This button works for saving satellite position graph into PC as png or jpg file
        private void buttonSavePosition_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg"; // File types: png, jpg
                saveFileDialog.Title = "Save Image";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmap = new Bitmap(pictureBoxSatelliteTrajectoryViewer.Width, pictureBoxSatelliteTrajectoryViewer.Height);

                    // Draw the trajectory of position onto the created bitmap 
                    pictureBoxSatelliteTrajectoryViewer.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBoxSatelliteTrajectoryViewer.Width, pictureBoxSatelliteTrajectoryViewer.Height));

                    // Save the bitmap to the selected file path
                    bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);

                    MessageBox.Show("Image saved successfully.");
                }
            }
        }

        // When this button clicked close " panelSatellitePositionTrajectoryViewer "
        private void buttonClosePosition_Click(object sender, EventArgs e)
        {
            panelSatellitePositionTrajectoryViewer.Visible = false;
        }

        #endregion

        // Region for storing buttons for velocity graph
        #region Button Velocity Graph

        // When this button clicked show the user " panelSatelliteVelocityTrajectoryViewer "
        private void buttonShowVelocityGraph_Click(object sender, EventArgs e)
        {
            // Check that is the simulation completed or not
            if (!simulationCompleted)
            {
                MessageBox.Show("Please simulate the program at least once before viewing the velocity graph.",
                                "Simulation Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            panelSatelliteVelocityTrajectoryViewer.Visible = true;
        }

        // This button works for saving satellite velocity graph into PC as png or jpg file
        private void buttonSaveVelocity_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg"; // File types: png, jpg
                saveFileDialog.Title = "Save Image";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmap = new Bitmap(pictureBoxSatelliteVelocityTrajectoryViewer.Width, pictureBoxSatelliteVelocityTrajectoryViewer.Height);

                    // Draw the trajectory of velocity onto the created bitmap 
                    pictureBoxSatelliteVelocityTrajectoryViewer.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBoxSatelliteVelocityTrajectoryViewer.Width, pictureBoxSatelliteVelocityTrajectoryViewer.Height));

                    // Save the bitmap to the selected file path
                    bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);

                    MessageBox.Show("Image saved successfully.");
                }
            }
        }

        // When this button clicked close " panelSatelliteVelocityTrajectoryViewer "
        private void buttonCloseVelocity_Click(object sender, EventArgs e)
        {
            panelSatelliteVelocityTrajectoryViewer.Visible = false;
        }

        #endregion

        // Region for storing buttons about updating satellite and ground station
        #region Update Buttons

        // When user click this button the satellite properties of the related satellite in the list is changing 
        private void buttonUpdateSatellite_Click(object sender, EventArgs e)
        {
            if (!ErrorHandlerSatellite(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Check is the satellite node selected or not
            if (treeViewSpaceSystem.SelectedNode != null && treeViewSpaceSystem.SelectedNode.Tag is Satellite satellite)
            {
                UpdateProperties(satellite);

                UpdateSatelliteInList(satellite);
                RescalePictureBasedOnSatelliteProperties(satellite);

                simulation.SaveSatelliteData(satellite);
                MessageBox.Show($"Satellite {satellite.Properties.SatelliteName} updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // When user click this button the ground station properties of the related ground station in the list is changing
        private void buttonUpdateGroundStation_Click(object sender, EventArgs e)
        {
            if (!ErrorHandlerGroundStation(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Check is the satellite node selected or not
            if (treeViewSpaceSystem.SelectedNode != null && treeViewSpaceSystem.SelectedNode.Tag is GroundStation groundStation)
            {

                UpdateProperties(groundStation);
                UpdateGroundStationInList(groundStation);
                pictureBoxGroundStation.Refresh();
                pictureBoxGroundStationViewer.Refresh();


                simulation.SaveGroundStationData(groundStation);
                MessageBox.Show($"Ground station {groundStation.GroundStationName} updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        // Region for storing buttons about opening form a file
        #region Open From File Buttons

        // When this button clicked user can input satellite data from a file
        private void buttonOpenFromFileSatellite_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("The file does not exist.");
                    return;
                }
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    if (lines.Length != 7) // Check the number of line
                    {
                        MessageBox.Show("The file does not have the expected number of lines. Please select a valid file.");
                        return;
                    }

                    // Check if the first line contains "ReceiverSatellite" or "SenderSatellite"
                    string firstLine = lines[0].Split(':')[1].Trim();
                    if (firstLine.Contains("ReceiverSatellite"))
                    {
                        radioButtonRS.Checked = true;
                    }
                    else if (firstLine.Contains("SenderSatellite"))
                    {
                        radioButtonSS.Checked = true;
                    }

                    // Fill the textBoxes with related data
                    textBoxSatelliteName.Text = lines[1].Split(':')[1].Trim();
                    textBoxOrbitRadius.Text = lines[2].Split(':')[1].Trim();
                    textBoxThetaAngle.Text = lines[3].Split(':')[1].Trim();
                    textBoxPhiAngle.Text = lines[4].Split(':')[1].Trim();
                    textBoxMass.Text = lines[5].Split(':')[1].Trim();
                    textBoxVelocity.Text = lines[6].Split(':')[1].Trim();
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"An error occurred while reading the file: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        // When this button clicked user can input ground station data from a file
        private void buttonOpenFromFileGS_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("The file does not exist.");
                    return;
                }

                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    if (lines.Length != 4) // Check the number of line
                    {
                        MessageBox.Show("The file does not have the expected number of lines. Please select a valid file.");
                        return;
                    }

                    // Check if the first line contains "C_GroundStation" or "T_GroundStation" or "BCT_GroundStation"
                    string firstLine = lines[0].Split(':')[1].Trim();
                    if (firstLine.Contains("C_GroundStation"))
                    {
                        radioButtonC_GroundStation.Checked = true;
                    }
                    else if (firstLine.Contains("T_GroundStation"))
                    {
                        radioButtonT_GroundStation.Checked = true;
                    }
                    else if (firstLine.Contains("BCT_GroundStation"))
                    {
                        radioButtonBCT_GroundStation.Checked = true;
                    }

                    // Fill the textBoxes with related data
                    textBoxGroundStationName.Text = lines[1].Split(':')[1].Trim();
                    textBoxGroundStationTheta.Text = lines[2].Split(':')[1].Trim();
                    textBoxGroundStationFoVAngle.Text = lines[3].Split(':')[1].Trim();
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"An error occurred while reading the file: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        #endregion

        // Region for storing buttons about saving to file
        #region Save to File Buttons

        // When this button clicked user can save the satellite data into a file
        private void buttonSaveToFileSatellite_Click(object sender, EventArgs e)
        {
            // Check if the satellite node is selected or not
            if (treeViewSpaceSystem.SelectedNode != null && treeViewSpaceSystem.SelectedNode.Tag is Satellite satellite)
            {
                DialogResult updateResult = MessageBox.Show("Do you want to update the satellite data before saving?",
                                                            "Update Satellite Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (updateResult == DialogResult.Yes)
                {
                    buttonUpdateSatellite_Click(sender, e);
                    AskUserCustomPath(satellite);
                    MessageBox.Show($"Properties of satellite {simulation.satellite.Properties.SatelliteName} is saved successfully.",
                                    "Satellite Properties Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (updateResult == DialogResult.No)
                {
                    if (!ErrorHandlerSatellite(out string errorMessage))
                    {
                        MessageBox.Show(errorMessage, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // Warn user about updating data
                    DialogResult saveResult = MessageBox.Show($"Warning: The satellite data may not be up to date. Proceeding to save the current data.\n\n" +
                                                              $"Do you want to save the satellite {simulation.satellite.Properties.SatelliteName} unsaved data into a file?",
                                                              "Save Satellite Data Into a File Without Updating", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (saveResult == DialogResult.Yes)
                    {
                        AskUserCustomPath(satellite);
                        simulation.SaveSatelliteData(satellite);
                        MessageBox.Show($"Properties of satellite {simulation.satellite.Properties.SatelliteName} is saved successfully.",
                                        "Satellite Properties Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        // When this button clicked user can save the ground station data into a file
        private void buttonSaveToFileGS_Click(object sender, EventArgs e)
        {
            // Check if the satellite node is selected or not
            if (treeViewSpaceSystem.SelectedNode != null && treeViewSpaceSystem.SelectedNode.Tag is GroundStation groundStation)
            {
                DialogResult updateResult = MessageBox.Show("Do you want to update the satellite data before saving?",
                                                            "Update Satellite Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (updateResult == DialogResult.Yes)
                {
                    buttonUpdateGroundStation_Click(sender, e);
                    AskUserCustomPath(groundStation);
                    MessageBox.Show($"Properties of ground station {groundStation.GroundStationName} is saved successfully.",
                                    "Ground Station Properties Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (updateResult == DialogResult.No)
                {
                    if (!ErrorHandlerGroundStation(out string errorMessage))
                    {
                        MessageBox.Show(errorMessage, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // Warn user about updating data
                    DialogResult saveResult = MessageBox.Show($"Warning: The Ground Station data may not be up to date. Proceeding to save the current data.\n\n" +
                                                              $"Do you want to save the Ground Station {groundStation.GroundStationName} unsaved data into a file?",
                                                              "Save Ground Station Data Into a File Without Updating", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (saveResult == DialogResult.Yes)
                    {
                        AskUserCustomPath(groundStation);
                        simulation.SaveGroundStationData(groundStation);
                        MessageBox.Show($"Properties of ground station {groundStation.GroundStationName} is saved successfully.",
                                        "Ground Station Properties Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion

        // Region for storing buttons about create (into list and tree view) satellite and ground station
        #region Create Buttons

        // When this button is clicked it add a satellite into list and add a child node of satellite into tree view 
        private void buttonCreateSatellite_Click(object sender, EventArgs e)
        {
            if (!ErrorHandlerSatellite(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UpdateSatelliteProperties();
            SatelliteRadioButtonsCheckHandler();

            if (simulation == null)
            {
                simulation = new Simulation(this, satelliteType, groundStationType);
            }
            else
            {
                simulation.satelliteType = satelliteType;
            }

            simulation.InitializeSatellite();
            AddSatelliteNode(simulation.satellite); // Create a satellite child node

            DialogResult result = MessageBox.Show($"Do you want to save the {simulation.satellite.Properties.SatelliteName} satellite data ?",
                                                  "Save Satellite Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                simulation.SaveSatelliteData(simulation.satellite);
            }
            ClearSatelliteProperties(); // After creating satellite clear the radio buttons and text boxes
        }

        // When this button is clicked it add a ground station into list and add a child node of ground station into tree view 
        private void buttonCreateGroundStation_Click(object sender, EventArgs e)
        {
            if (!ErrorHandlerGroundStation(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UpdateGroundStationProperties();
            GroundStationRadioButtonsCheckHandler();

            if (simulation == null)
            {
                simulation = new Simulation(this, satelliteType, groundStationType);
            }
            else
            {
                simulation.groundStationType = groundStationType;
            }
            simulation.InitializeGroundStation();
            AddGroundStationNode(simulation.groundStation); // Create a ground station child node
            DialogResult result = MessageBox.Show($"Do you want to save the {simulation.groundStation.GroundStationName} ground station data?",
                                                  "Save Ground Station Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                simulation.SaveGroundStationData(simulation.groundStation);
            }
            ClearGroundStationProperties(); // After creating ground station clear the radio buttons and text boxes
        }

        #endregion

        // Region for storing buttons about Opening Report File
        #region Report File Buttons

        // When this button is clicked show user to satellite report
        private void buttonGetSatelliteReport_Click(object sender, EventArgs e)
        {
            // Check if the simulation has been started
            if (!simulationCompleted)
            {
                MessageBox.Show("Before getting satellite report, please start the simulation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method
            }
            // Check if a satellite node is selected
            if (treeViewSpaceSystem.SelectedNode != null && treeViewSpaceSystem.SelectedNode.Tag is Satellite satellite)
            {
                int index = simulation.SatelliteList.IndexOf(satellite);
                string satelliteFolderPath = Path.Combine("SimulationDataFolder", "Satellites", $"Satellite_{index + 1}");
                string reportFilePath = Path.Combine(satelliteFolderPath, "SatelliteReportFile.txt");

                // Check if the report file exists
                if (File.Exists(reportFilePath))
                {
                    // Read the content of the report file
                    string reportContent = File.ReadAllText(reportFilePath);

                    // Display the content in a MessageBox
                    MessageBox.Show(reportContent, "Satellite Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Report file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // When this button is clicked show user to ground station report
        private void buttonGetGroundStationReport_Click(object sender, EventArgs e)
        {
            // Check if the simulation has been started
            if (!simulationCompleted)
            {
                MessageBox.Show("Before getting ground station report, please start the simulation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            // Check if a satellite node is selected
            if (treeViewSpaceSystem.SelectedNode != null && treeViewSpaceSystem.SelectedNode.Tag is GroundStation groundStation)
            {
                int index = simulation.GroundStationList.IndexOf(groundStation);
                string groundStationFolderPath = Path.Combine("SimulationDataFolder", "Ground Station", $"GroundStation_{index + 1}");
                string reportFilePath = Path.Combine(groundStationFolderPath, "GroundStationReportFile.txt");

                // Check if the report file exists
                if (File.Exists(reportFilePath))
                {
                    // Read the content of the report file
                    string reportContent = File.ReadAllText(reportFilePath);

                    // Display the content in a MessageBox
                    MessageBox.Show(reportContent, "Satellite Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Report file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        private bool simulationCompleted = false;

        // When this button is clicked the simulation is starting
        private void buttonStartSimulation_Click(object sender, EventArgs e)
        {
            pictureBoxSatelliteTrajectoryViewer.Refresh();
            if (!ErrorHandlerSimulation(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult startResult = MessageBox.Show("Do you want to start the simulation?",
                                                        "Start Simulation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (startResult == DialogResult.Yes)
            {
                UpdateSimulationDataProperties();

                MessageBox.Show("Simulation started successfully!",
                                "Simulation Started", MessageBoxButtons.OK, MessageBoxIcon.Information);

                simulation.InitializeSimulation();

                MessageBox.Show("Simulation finished successfully!",
                                "Simulation Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

                simulationCompleted = true;
                buttonShowPositionGraph.Visible = true;
            }
        }

        #endregion

        // Region for store methods of error and radio button handlers
        #region Handlers

        // Region for store error handler methods
        #region Error Handlers

        // Error handler for satellite this method checks that missing field
        // If found some miss field it returns an error message
        private bool ErrorHandlerSatellite(out string errorMessage)
        {
            // Collect all the missing parts as a list of string
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBoxOrbitRadius.Text))
            {
                missingFields.Add("Orbit Radius");
            }
            else
            {
                if (!double.TryParse(textBoxOrbitRadius.Text, out double newOrbitRadius))
                {
                    missingFields.Add("Orbit Radius (invalid format)");
                }
                else if (newOrbitRadius < 6530000)
                {
                    errorMessage = "The satellite altitude should be above 6530000 meters.";
                    return false;
                }
                else
                {
                    OrbitRadius = newOrbitRadius;
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxThetaAngle.Text))
            {
                missingFields.Add("Theta Angle");
            }
            else
            {
                if (!double.TryParse(textBoxThetaAngle.Text, out double thetaAngle))
                {
                    missingFields.Add("Theta Angle (invalid format)");
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxPhiAngle.Text))
            {
                missingFields.Add("Phi Angle");
            }
            else
            {
                if (!double.TryParse(textBoxPhiAngle.Text, out double phiAngle))
                {
                    missingFields.Add("Phi Angle (invalid format)");
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxMass.Text))
            {
                missingFields.Add("Mass");
            }
            else
            {
                if (!double.TryParse(textBoxMass.Text, out double mass))
                {
                    missingFields.Add("Mass (invalid format)");
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxVelocity.Text))
            {
                missingFields.Add("Velocity");
            }
            else
            {
                if (!double.TryParse(textBoxVelocity.Text, out double velocity))
                {
                    missingFields.Add("Velocity (invalid format)");
                }
            }

            if (!radioButtonRS.Checked && !radioButtonSS.Checked)
            {
                missingFields.Add("Satellite Type");
            }

            if (missingFields.Count > 0)
            {
                errorMessage = "Please fill in the following fields: " + string.Join(", ", missingFields) + ".";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }


        // Error handler for ground station this method checks that missing field
        // If found some miss field it returns an error message
        private bool ErrorHandlerGroundStation(out string errorMessage)
        {
            // Collect all the missing parts as a list of string
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBoxGroundStationTheta.Text))
            {
                missingFields.Add("Theta Angle");
            }
            else
            {
                if (!double.TryParse(textBoxGroundStationTheta.Text, out double groundStationTheta))
                {
                    missingFields.Add("Theta Angle (invalid format)");
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxGroundStationFoVAngle.Text))
            {
                missingFields.Add("Field of View Angle");
            }
            else
            {
                if (!double.TryParse(textBoxGroundStationFoVAngle.Text, out double groundStationFoVAngle))
                {
                    missingFields.Add("Field of View Angle (invalid format)");
                }
            }

            if (!radioButtonBCT_GroundStation.Checked && !radioButtonC_GroundStation.Checked && !radioButtonT_GroundStation.Checked)
            {
                missingFields.Add("Ground Station Type");
            }

            if (missingFields.Count > 0)
            {
                errorMessage = "Please fill in the following fields: " + string.Join(", ", missingFields) + ".";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }


        // Error handler for simulation this method checks that missing field
        // If found some miss field it returns an error message
        private bool ErrorHandlerSimulation(out string errorMessage)
        {
            List<string> missingFields = new List<string>();
            List<string> missingFields2 = new List<string>();

            if (simulation == null)
            {
                errorMessage = "Before starting the simulation, please add at least one: Ground Station, Satellite.";
                return false;
            }

            if (simulation.SatelliteList == null || simulation.SatelliteList.Count == 0)
            {
                missingFields.Add("Satellite");
            }

            if (simulation.GroundStationList == null || simulation.GroundStationList.Count == 0)
            {
                missingFields.Add("Ground Station");
            }

            if (missingFields.Count > 0)
            {
                errorMessage = "Before starting the simulation, please add at least one: " + string.Join(", ", missingFields);
                return false;
            }

            // Check integration method, simulation time, and time step fields
            if (!radioButtonEuler.Checked && !radioButtonRungeKutta.Checked)
            {
                missingFields2.Add("Integration Method");
            }

            if (string.IsNullOrWhiteSpace(textBoxSimulationTime.Text))
            {
                missingFields2.Add("Simulation Time");
            }
            else
            {
                if (!double.TryParse(textBoxSimulationTime.Text, out double simulationTime))
                {
                    missingFields2.Add("Simulation Time (invalid format)");
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxTimeStep.Text))
            {
                missingFields2.Add("Time Step");
            }
            else
            {
                if (!double.TryParse(textBoxTimeStep.Text, out double timeStep))
                {
                    missingFields2.Add("Time Step (invalid format)");
                }
            }

            if (missingFields2.Count > 0)
            {
                errorMessage = "Before starting the simulation, please fill in the following fields: " + string.Join(", ", missingFields2);
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }


        #endregion 

        // Region for store radio button handler methods
        #region Radio Button Handlers

        // Change the satellite type wrt radio button
        void SatelliteRadioButtonsCheckHandler()
        {
            if (radioButtonRS.Checked)
            {
                satelliteType = SatelliteType.ReceiverSatellite;
            }
            else if (radioButtonSS.Checked)
            {
                satelliteType = SatelliteType.SenderSatellite;
            }
            else
            {
                return;
            }
        }

        // Change the ground station type wrt radio button
        void GroundStationRadioButtonsCheckHandler()
        {
            if (radioButtonBCT_GroundStation.Checked)
            {
                groundStationType = GroundStationType.BCT_GroundStation;
            }
            else if (radioButtonC_GroundStation.Checked)
            {
                groundStationType = GroundStationType.C_GroundStation;
            }
            else if (radioButtonT_GroundStation.Checked)
            {
                groundStationType = GroundStationType.T_GroundStation;
            }
            else
            {
                return;
            }
        }

        #endregion

        #endregion

        // Region for store methods about updating something (satellite, ground station, simulation, lists etc.)
        #region Field Updater

        // Region for storing method about satellite update field 
        #region Satellite Field Updater

        // Method for updating satellite properties with the entered text box values before creating a satellite into a list.
        // This method works when button create satellite is clicked
        void UpdateSatelliteProperties()
        {
            if (double.TryParse(textBoxOrbitRadius.Text, out double newOrbitRadius))
            {
                OrbitRadius = newOrbitRadius;
                // Redraw the satellite wrt satellite radius
                drawSatellite.RescalePicture(newOrbitRadius, originalWorldSize, originalSatelliteSize, originalPixelScale);
            }

            if (double.TryParse(textBoxThetaAngle.Text, out double newThetaAngle))
            {
                SatelliteThetaAngle = newThetaAngle;
            }

            if (double.TryParse(textBoxPhiAngle.Text, out double newPhiAngle))
            {
                PhiAngle = newPhiAngle;
            }

            if (double.TryParse(textBoxMass.Text, out double newMass))
            {
                Mass = newMass;
            }

            if (double.TryParse(textBoxVelocity.Text, out double newVelocity))
            {
                Velocity = newVelocity;
            }
        }

        // Method for writing related satellite (selected tree view satellite child) data into text boxes
        private void UpdateProperties(Satellite satellite)
        {
            satellite.Properties.InitialOrbitRadius = Convert.ToDouble(textBoxOrbitRadius.Text);
            satellite.Properties.InitialThetaAngle = Convert.ToDouble(textBoxThetaAngle.Text);
            satellite.Properties.InitialPhiAngle = Convert.ToDouble(textBoxPhiAngle.Text);
            satellite.Properties.SatelliteMass = Convert.ToDouble(textBoxMass.Text);
            satellite.Properties.SatelliteInitialVelocity = Convert.ToDouble(textBoxVelocity.Text);

            SatelliteRadioButtonsCheckHandler();
            satellite.CalculateComponents();
        }

        // Method for clearing radio buttons, text boxes
        void ClearSatelliteProperties()
        {
            // Clear the radio buttons
            radioButtonSS.Checked = false;
            radioButtonRS.Checked = false;

            // Set textBox values as null
            textBoxOrbitRadius.Text = "";
            textBoxThetaAngle.Text = "";
            textBoxPhiAngle.Text = "";
            textBoxMass.Text = "";
            textBoxVelocity.Text = "";

            // Default values
            double defaultOrbitRadius = 11000000;

            drawSatellite.ThetaAngle = 0;
            drawSatellite.PhiAngle = 0;
            pictureBoxSatelliteViewer.Refresh();

            drawSatellite.RescalePicture(
                defaultOrbitRadius,
                originalWorldSize,
                originalSatelliteSize,
                originalPixelScale
            );
        }

        #endregion

        // Region for storing method about ground station update field
        #region Ground Station Field Updater

        // Method for updating ground station properties with the entered text box values before creating a ground station into a list.
        // This method works when button create ground station is clicked
        void UpdateGroundStationProperties()
        {
            if (double.TryParse(textBoxGroundStationTheta.Text, out double newThetaAngle))
            {
                GroundStationThetaAngle = newThetaAngle;
            }
            if (double.TryParse(textBoxGroundStationFoVAngle.Text, out double newFoVAngle))
            {
                FieldOfViewAngle = newFoVAngle;
            }
        }

        // Method for writing related ground station (selected tree view ground station child) data into text boxes
        private void UpdateProperties(GroundStation groundStation)
        {
            groundStation.ThetaAngle = Convert.ToDouble(textBoxGroundStationTheta.Text);
            groundStation.FieldOfViewAngle = Convert.ToDouble(textBoxGroundStationFoVAngle.Text);
            GroundStationRadioButtonsCheckHandler();
            groundStation.CalculateLocation();
        }

        // Method for clearing radio buttons, text boxes
        void ClearGroundStationProperties()
        {
            radioButtonBCT_GroundStation.Checked = false;
            radioButtonC_GroundStation.Checked = false;
            radioButtonT_GroundStation.Checked = false;

            textBoxGroundStationTheta.Text = "";
            textBoxGroundStationFoVAngle.Text = "";

            drawGroundStation.GroundStationThetaAngle = 0;
            drawGroundStation.FieldOfViewAngle = 0;
            pictureBoxGroundStationViewer.Refresh();
            pictureBoxWorld_2.Refresh();
        }

        #endregion

        // Region for storing method about simulation update field
        #region Simulation Field Updater

        // Method for updating simulation properties with the entered text box values before starting the simulation.
        // This method works when button simulation is clicked
        void UpdateSimulationDataProperties()
        {
            // Update FinalTime based on user input
            if (double.TryParse(textBoxSimulationTime.Text, out double newFinalTime))
            {
                FinalTime = newFinalTime;
            }
            // Update TimeStep based on user input
            if (double.TryParse(textBoxTimeStep.Text, out double newTimeStep))
            {
                TimeStep = newTimeStep;
            }
        }

        #endregion

        // Region for storing method about updating lists 
        #region Update Lists

        // When update satellite button is clicked and update the related satellite with new entered values and store in the list
        private void UpdateSatelliteInList(Satellite updatedSatellite)
        {
            // Find the index of the updated satellite in the list
            int index = simulation.SatelliteList.FindIndex(satellite => satellite.Equals(updatedSatellite));

            if (index != -1)
            {
                // update the satellite related index with new entered properties
                simulation.SatelliteList[index] = updatedSatellite;

                if (simulation != null && simulation.SatelliteList.Count > index)
                {
                    simulation.SatelliteList[index] = updatedSatellite;
                }
            }
        }

        // When update groudn station button is clicked and update the related satellite with new entered values and store in the list
        private void UpdateGroundStationInList(GroundStation updatedGroundStation)
        {
            // Find the index of the updated satellite in the list
            int index = simulation.GroundStationList.FindIndex(groundStation => groundStation.Equals(updatedGroundStation));

            if (index != -1)
            {    // update the satellite related index with new entered properties
                simulation.GroundStationList[index] = updatedGroundStation;

                // If the simulation exists, update the satellite object in the simulation as well
                if (simulation != null && simulation.GroundStationList.Count > index)
                {
                    simulation.GroundStationList[index] = updatedGroundStation;
                }
            }
        }

        #endregion

        #endregion

        // Region for storing method about to control tree view
        #region Tree View Controller 

        // Method that control what is going to happen when tree view is selected
        private void treeViewSpaceSystem_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Check if the selected node is the "SpaceSystem" node
            if (e.Node.Name == "SpaceSystem")
            {
                panelMainPage.Visible = true;
                panelSatellite.Visible = false;
                panelGroundStation.Visible = false;

            }
            // Check if the selected node is the "Satellites" node
            else if (e.Node.Name == "Satellite")
            {
                buttonGetSatelliteReport.Visible = false; 
                buttonGetGroundStationReport.Visible = false;
                panelMainPage.Visible = false;
                buttonShowPositionGraph.Visible = false;
                buttonShowVelocityGraph.Visible = false;
                radioButtonRS.Enabled = true;
                radioButtonSS.Enabled = true;
                panelSatellite.Visible = true;
                panelGroundStation.Visible = false;
                panelSatellitePositionTrajectoryViewer.Visible = false;
                buttonCreateSatellite.Visible = true;
                buttonOpenFromFileSatellite.Visible = true;
                buttonUpdateSatellite.Visible = false;
                buttonSaveToFileSatellite.Visible = false;
                labelSatelliteName.Visible = false;
                textBoxSatelliteName.Visible = false;
                ClearSatelliteProperties();
            }
            // Check if the selected node is the "GroundStation" node
            else if (e.Node.Name == "GroundStation")
            {
                buttonGetSatelliteReport.Visible = false;
                buttonGetGroundStationReport.Visible = false;
                panelMainPage.Visible = false;
                buttonShowPositionGraph.Visible = false;
                buttonShowVelocityGraph.Visible = false;
                radioButtonC_GroundStation.Enabled = true;
                radioButtonT_GroundStation.Enabled = true;
                radioButtonBCT_GroundStation.Enabled = true;
                panelSatellite.Visible = false;
                panelGroundStation.Visible = true;
                buttonCreateGroundStation.Visible = true;
                buttonOpenFromFileGS.Visible = true;
                buttonUpdateGroundStation.Visible = false;
                buttonSaveToFileGS.Visible = false;
                labelGroundStationName.Visible = false;
                textBoxGroundStationName.Visible = false;
                ClearGroundStationProperties();
            }
            // Check if the selected node is a child of "Satellite" node
            if (e.Node.Tag is Satellite satellite)
            {
                buttonGetSatelliteReport.Visible = true;
                buttonGetGroundStationReport.Visible = false;
                panelMainPage.Visible = false;
                panelMainPage.Visible = false;
                labelVelocityGraph.Text = $"{satellite.Properties.SatelliteName} Velocity Graph";
                labelPositionGraph.Text = $"{satellite.Properties.SatelliteName} Position Graph";
                buttonShowPositionGraph.Visible = true;
                buttonShowVelocityGraph.Visible = true;
                radioButtonRS.Enabled = false;
                radioButtonSS.Enabled = false;
                panelSatellite.Visible = true;
                panelGroundStation.Visible = false;
                buttonCreateSatellite.Visible = false;
                buttonOpenFromFileSatellite.Visible = false;
                buttonUpdateSatellite.Visible = true;
                buttonSaveToFileSatellite.Visible = true;
                labelSatelliteName.Visible = true;
                textBoxSatelliteName.Visible = true;
                textBoxSatelliteName.Text = satellite.Properties.SatelliteName;
                DisplayProperties(satellite);
                RescalePictureBasedOnSatelliteProperties(satellite);
                pictureBoxSatelliteTrajectoryViewer.Refresh();
            }
            // Check if the selected node is a child of "Ground Station" node 
            if (e.Node.Tag is GroundStation groundStation)
            {
                buttonGetSatelliteReport.Visible = false;
                buttonGetGroundStationReport.Visible = true;
                panelMainPage.Visible = false;
                buttonShowVelocityGraph.Visible = false;
                buttonShowPositionGraph.Visible = false;
                radioButtonC_GroundStation.Enabled = false;
                radioButtonT_GroundStation.Enabled = false;
                radioButtonBCT_GroundStation.Enabled = false;
                panelSatellite.Visible = false;
                panelGroundStation.Visible = true;
                buttonCreateGroundStation.Visible = false;
                buttonOpenFromFileGS.Visible = false;
                buttonUpdateGroundStation.Visible = true;
                buttonSaveToFileGS.Visible = true;
                labelGroundStationName.Visible = true;
                textBoxGroundStationName.Visible = true;
                textBoxGroundStationName.Text = groundStation.GroundStationName;

                DisplayProperties(groundStation);
                pictureBoxGroundStation.Refresh();
                pictureBoxWorld_2.Refresh();
            }
        }

        // Region for storing method about tree view child node adder
        #region Node Adder

        // Method for creating a child node of "Satellites"
        private void AddSatelliteNode(Satellite satellite)
        {
            // Find the "SpaceSystem" node
            TreeNode spaceSystemNode = treeViewSpaceSystem.Nodes["SpaceSystem"];

            if (spaceSystemNode != null)
            {
                // Find the "Satellites" child node of "SpaceSystem"
                TreeNode satellitesNode = spaceSystemNode.Nodes["Satellite"];
                if (satellitesNode == null)
                {
                    // If "Satellites" node doesn't exist, create it
                    satellitesNode = new TreeNode("Satellite")
                    {
                        Name = "Satellites"
                    };
                    spaceSystemNode.Nodes.Add(satellitesNode);
                }

                // Add the new satellite node
                TreeNode newSatelliteNode = new TreeNode(satellite.Properties.SatelliteName)
                {
                    Tag = satellite, // Store the satellite object in the Tag property
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };
                satellitesNode.Nodes.Add(newSatelliteNode);

                // Expand the "Satellites" node to show the new created child node
                satellitesNode.Expand();
            }
            else
            {
                MessageBox.Show("SpaceSystem node not found in the tree view.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method for creating a child node of "GroundStation"
        private void AddGroundStationNode(GroundStation groundStation)
        {
            // Find the "SpaceSystem" node
            TreeNode spaceSystemNode = treeViewSpaceSystem.Nodes["SpaceSystem"];
            if (spaceSystemNode != null)
            {
                // Find the "GroundStation" child node of "SpaceSystem"
                TreeNode groundStationsNode = spaceSystemNode.Nodes["GroundStation"];
                if (groundStationsNode == null)
                {
                    // If "GroundStation" node doesn't exist, create it
                    groundStationsNode = new TreeNode("GroundStation")
                    {
                        Name = "Ground Stations"
                    };
                    spaceSystemNode.Nodes.Add(groundStationsNode);
                }

                // Add the new ground station node
                TreeNode newGroundStationNode = new TreeNode(groundStation.GroundStationName)
                {
                    Tag = groundStation, // Store the ground station object in the Tag property
                    ImageIndex = 2,
                    SelectedImageIndex = 2
                };
                groundStationsNode.Nodes.Add(newGroundStationNode);

                // Expand the "GroundStation" node to show the new created child node
                newGroundStationNode.Expand();
            }
            else
            {
                MessageBox.Show("SpaceSystem node not found in the tree view.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        // Method for updating progress of progress bar 
        public void UpdateProgress(double progressPercentage)
        {
            progressPercentage = Math.Max(0, Math.Min(100, progressPercentage));

            // Update the progressBarSimulation value
            progressBarSimulation.Value = (int)progressPercentage;
            Application.DoEvents();
        }

        // Method for saving satellite into custom path
        void AskUserCustomPath(Satellite satellite)
        {
            DialogResult addressChoiceResult = MessageBox.Show("Do you want to choose the address to save the file?",
                                                                "Choose Address", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (addressChoiceResult == DialogResult.Yes)
            {
                simulation.ChooseCustomAddress(satellite);
            }
            else
            {
                simulation.SaveSatelliteData(satellite);
            }
        }

        // Method for saving ground station into custom path
        void AskUserCustomPath(GroundStation groundStation)
        {
            DialogResult addressChoiceResult = MessageBox.Show("Do you want to choose the address to save the file?",
                                                                "Choose Address", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (addressChoiceResult == DialogResult.Yes)
            {
                simulation.ChooseCustomAddress(groundStation);
            }
            else
            {
                simulation.SaveGroundStationData(groundStation);
            }
        }

    }

    // This region stores the Simulation class
    #region Simulation
    public class Simulation
    {
        public double FinalTime { get; set; } // Simulation time (user entered)
        public double TimeStep { get; set; } // Time step of the simulation (user entered)
        public double InitialTime { get; set; } // Initial time (it is predefined as 0)

        public MainForm form;
        public Satellite satellite;
        public GroundStation groundStation;
        public TotalForceCalculator totalForceCalculator;
        public SatelliteType satelliteType;
        public GroundStationType groundStationType;

        public List<Satellite> SatelliteList { get; set; } // List that store the created satellite
        public List<GroundStation> GroundStationList { get; set; } // List that store the created ground station

        public Dictionary<Satellite, List<(double X, double Y)>> SatellitePositions { get; set; } // Dictionary that stores X and Y values of satellite
        public Dictionary<Satellite, List<(double Vx, double Vy)>> SatelliteVelocity { get; set; } // Dictionary that stores Vx and Vy values of satellite
        public Simulation(MainForm form, SatelliteType satelliteType, GroundStationType groundStationType)
        {
            this.form = form;
            this.satelliteType = satelliteType;
            this.groundStationType = groundStationType;
            SatelliteList = new List<Satellite>();
            GroundStationList = new List<GroundStation>();
            SatellitePositions = new Dictionary<Satellite, List<(double X, double Y)>>();
            SatelliteVelocity = new Dictionary<Satellite, List<(double Vx, double Vy)>>();
        }

        // This method initialize the simulation
        public void InitializeSimulation()
        {
            FinalTime = form.FinalTime; // Get final time entered by user
            TimeStep = form.TimeStep;   // Get time step entered by user

            int totalSteps = SatelliteList.Count; // Number of satellite in the list (for progress bar calculation)
            int stepsCompleted = 0;

            foreach (Satellite satellite in SatelliteList)
            {
                satellite.CalculateComponents(); // Calculate satellite components
                GravityModel gravityModel = new GravityModel(satellite);
                AerodynamicModel aerodynamicModel = new AerodynamicModel(satellite);
                this.totalForceCalculator = new TotalForceCalculator(gravityModel, aerodynamicModel);

                double progressPercentage = (double)stepsCompleted / totalSteps * 100.0; // Calculate progress bar percentage
                form.UpdateProgress(progressPercentage); // Update progress in the form

                // Check integration method and use related method 
                if (form.radioButtonRungeKutta.Checked)
                {
                    Integrate_RungeKutta4thOrder(satellite);
                }
                if (form.radioButtonEuler.Checked)
                {
                    Integrate_Euler(satellite);
                }
                // After finishing integration check satellite is in the field of ground station or not
                IsInFieldOfView(satellite, GroundStationList);
                stepsCompleted++; // Increase step
            }
            form.UpdateProgress(100);
        }

        // Region for storing ground station simulation methods
        #region Ground Station Related Methods 

        // This method initialize the ground stations
        public void InitializeGroundStation()
        {
            // Get values from text boxes
            double thetaAngle = form.GroundStationThetaAngle;
            double fieldOfViewAngle = form.FieldOfViewAngle;

            // Check ground station type and initialize the related ground station
            if (groundStationType == GroundStationType.C_GroundStation)
            {
                groundStation = new C_GroundStation(groundStationType, thetaAngle, fieldOfViewAngle);
            }
            else if (groundStationType == GroundStationType.T_GroundStation)
            {
                groundStation = new T_GroundStation(groundStationType, thetaAngle, fieldOfViewAngle);
            }
            else if (groundStationType == GroundStationType.BCT_GroundStation)
            {
                groundStation = new BCT_GroundStation(groundStationType, thetaAngle, fieldOfViewAngle);
            }
            else
            {
                return;
            }
            GroundStationList.Add(groundStation); // After initializing the ground station, add it to the list
        }

        // This method saving the ground station data to a file
        public void SaveGroundStationData(GroundStation saveGroundStation)
        {
            int index = GroundStationList.FindIndex(groundStation => groundStation.Equals(saveGroundStation));

            string groundStationFolderPath = Path.Combine("SimulationDataFolder", "Ground Station", $"GroundStation_{index + 1}");

            DataWriter dataWriter = new DataWriter();
            dataWriter.WriteGroundStationDataToFile(saveGroundStation, groundStationFolderPath);
        }

        // This method saving the ground station report to a file
        public void SaveGroundStationReport(Satellite satellite, GroundStation saveGroundStation, string message)
        {
            int index = GroundStationList.FindIndex(groundStation => groundStation.Equals(saveGroundStation));
            string groundStationFolderPath = Path.Combine("SimulationDataFolder", "Ground Station", $"GroundStation_{index + 1}");

            DataWriter dataWriter = new DataWriter();
            string groundStationReportFilePath = Path.Combine(groundStationFolderPath, "GroundStationReportFile.txt");

            if (saveGroundStation is T_GroundStation || saveGroundStation is BCT_GroundStation)
            {
                dataWriter.WriteGroundStationReportToFile(saveGroundStation, groundStationReportFilePath, message);
            }

            if ((saveGroundStation is C_GroundStation || saveGroundStation is BCT_GroundStation) && satellite is SenderSatellite)
            {
                message = $"Collected data successfully gathered from {satellite.Properties.SatelliteName}. ";
                dataWriter.WriteGroundStationReportToFile(saveGroundStation, groundStationReportFilePath, message);
            }
        }

        // This method saving the ground station into custom path
        public string ChooseCustomAddress(GroundStation saveGroundStation)
        {
            int index = GroundStationList.FindIndex(groundStation => groundStation.Equals(saveGroundStation));
            string groundStationFolderPath;

            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    groundStationFolderPath = folderDialog.SelectedPath;
                    DataWriter dataWriter = new DataWriter();
                    dataWriter.WriteGroundStationDataToFile(saveGroundStation, groundStationFolderPath);
                }
                else
                {
                    groundStationFolderPath = Path.Combine("SimulationDataFolder", "Satellites", $"Satellite_{index + 1}");
                }
            }
            return groundStationFolderPath;
        }

        #endregion

        // Region for storing satellite simulation methods
        #region Satellite Related Methods

        // This method initialize the satellite
        public void InitializeSatellite()
        {
            // Get values from text boxes
            double initialOrbitRadius = form.OrbitRadius;
            double initialThetaAngle = form.SatelliteThetaAngle;
            double initialPhiAngle = form.PhiAngle;
            double satelliteMass = form.Mass;
            double satelliteInitialVelocity = form.Velocity;

            // Check satellite type and initialize the related satellite
            if (satelliteType == SatelliteType.ReceiverSatellite)
            {
                satellite = new ReceiverSatellite(satelliteType, initialOrbitRadius, initialThetaAngle, initialPhiAngle, satelliteMass, satelliteInitialVelocity);
            }
            else if (satelliteType == SatelliteType.SenderSatellite)
            {
                satellite = new SenderSatellite(satelliteType, initialOrbitRadius, initialThetaAngle, initialPhiAngle, satelliteMass, satelliteInitialVelocity);
            }
            else
            {
                return;
            }
            // After initializing the satellite, add it to the list, and SatellitePositions, SatelliteVelocity Dictionaries
            SatelliteList.Add(satellite);
            SatellitePositions[satellite] = new List<(double X, double Y)>();
            SatelliteVelocity[satellite] = new List<(double Vx, double Vy)>();
        }

        // This method saving the satellite data to a file
        public void SaveSatelliteData(Satellite saveSatellite)
        {
            int index = SatelliteList.FindIndex(satellite => satellite.Equals(saveSatellite));

            string satelliteFolderPath = Path.Combine("SimulationDataFolder", "Satellites", $"Satellite_{index + 1}");

            DataWriter dataWriter = new DataWriter();
            dataWriter.WriteSatelliteDataToFile(saveSatellite, satelliteFolderPath);
        }

        // This method saving the satellite report to a file
        public void SaveSatelliteReport(Satellite saveSatellite, GroundStation groundStation, string message)
        {
            int index = SatelliteList.FindIndex(satellite => satellite.Equals(saveSatellite));

            string satelliteFolderPath = Path.Combine("SimulationDataFolder", "Satellites", $"Satellite_{index + 1}");

            DataWriter dataWriter = new DataWriter();
            string satelliteReportFilePath = Path.Combine(satelliteFolderPath, "SatelliteReportFile.txt");
            if ((saveSatellite is ReceiverSatellite) && (groundStation is C_GroundStation || groundStation is BCT_GroundStation))
            {
                message = $"Some data received from {groundStation.GroundStationName} ground station.";
                dataWriter.WriteSatelliteReportToFile(saveSatellite, satelliteReportFilePath, message);
            }
        }

        // This method saving the satellite into custom path
        public string ChooseCustomAddress(Satellite saveSatellite)
        {
            int index = SatelliteList.FindIndex(satellite => satellite.Equals(saveSatellite));
            string satelliteFolderPath;

            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    satelliteFolderPath = folderDialog.SelectedPath;
                    DataWriter dataWriter = new DataWriter();
                    dataWriter.WriteSatelliteDataToFile(saveSatellite, satelliteFolderPath);
                }
                else
                {
                    satelliteFolderPath = Path.Combine("SimulationDataFolder", "Satellites", $"Satellite_{index + 1}");
                }
            }

            return satelliteFolderPath;
        }

        #endregion

        // Region for storing integration methods and derivative method
        #region Integration

        // This method calculate the derivative of the state vector of the satellite
        private double[] Derivative(Satellite satellite)
        {
            // X0[0] = Position_x, X0[1] = Position_y, X0[2] = Velocity_x, X0[3] = Velocity_y
            // Xdot[0] = Velocity_x, Xdot[1] = Velocity_y, Xdot[2] = Acceleration_x, Xdot[3] = Acceleration_y
            satellite.Properties.Xdot[0] = satellite.Properties.X0[2];
            satellite.Properties.Xdot[1] = satellite.Properties.X0[3];
            satellite.Properties.Xdot[2] = (1.0 / satellite.Properties.SatelliteMass)
                                           * totalForceCalculator.TotalForce_XComponent; // Acceleration_x = 1/m * TotalForce_x
            satellite.Properties.Xdot[3] = (1.0 / satellite.Properties.SatelliteMass)
                                           * totalForceCalculator.TotalForce_YComponent; // Acceleration_y = 1/m * TotalForce_y

            return satellite.Properties.Xdot;
        }

        /// This method uses Euler integration method to calculate the integral of a  
        /// function in the determined interval with the specified time increment
        public void Integrate_Euler(Satellite satellite)
        {
            DataWriter dataWriter = new DataWriter();
            InitialTime = 0;

            int n = (int)(Math.Ceiling((FinalTime - InitialTime) / TimeStep));

            double[] X = new double[satellite.Properties.X0.Length];

            // Create folder for the satellite
            string satelliteFolderPath = Path.Combine("SimulationDataFolder", "Satellites", $"Satellite_{SatelliteList.IndexOf(satellite) + 1}");
            Directory.CreateDirectory(satelliteFolderPath);

            string simulationDataFilePath = Path.Combine(satelliteFolderPath, "SimulationData.txt");
            using (StreamWriter writer = new StreamWriter(simulationDataFilePath))
            {
                writer.Write("Time" + "   " + "Position X" + "   " + "Position Y" + "   " + "Velocity X" + "   " + "Velocity Y" + "   " + "theta angle" + "   " + "beta angle");
                writer.WriteLine();
                for (int i = 0; i < X.Length; i++) X[i] = satellite.Properties.X0[i]; // 

                for (int i = 0; i < n; i++)
                {
                    // Calculate derivatives of the state vector
                    double[] Xd = Derivative(satellite);
                    for (int j = 0; j < X.Length; j++)
                    {
                        // X[0] = Position_x, X[1] = Position_y, X[2] = Velocity_x, X[3] = Velocity_y
                        // Xdot[0] = Velocity_x, Xdot[1] = Velocity_y, Xdot[2] = Acceleration_x, Xdot[3] = Acceleration_y
                        // Position_x(n) = Position_x(n-1) + Velocity_x(n-1) * TimeStep
                        // Position_y(n) = Position_y(n-1) + Velocity_y(n-1) * TimeStep
                        // Velocity_x(n) = Velocity_x(n-1) + Acceleration_x(n-1) * TimeStep
                        // Velocity_y(n) = Velocity_y(n-1) + Acceleration_y(n-1) * TimeStep
                        X[j] += Xd[j] * TimeStep;
                    }
                    SatellitePositions[satellite].Add((X[0], X[1]));
                    SatelliteVelocity[satellite].Add((X[2], X[3]));
                    double thetaAngle = totalForceCalculator.GravityModel.ThetaAngle;
                    double betaAngle = totalForceCalculator.AerodynamicModel.BetaAngle;
                    dataWriter.WriteSimulationDataToFile(InitialTime, X, thetaAngle, betaAngle, writer);
                    InitialTime += TimeStep;
                    for (int j = 0; j < X.Length; j++) satellite.Properties.X0[j] = X[j];
                    totalForceCalculator.CalculateTotalForce(); // Recalculate total force after founding new state vector
                }
                writer.Close();
            }
        }
        
        /// This method uses Runge-Kutta 4th Order integration method to calculate the integral of a  
        /// function in the determined interval with the specified time increment
        public void Integrate_RungeKutta4thOrder(Satellite satellite)
        {
            DataWriter dataWriter = new DataWriter();
            InitialTime = 0;

            int n = (int)(Math.Ceiling((FinalTime - InitialTime) / TimeStep));

            double[] X = new double[satellite.Properties.X0.Length];
            double[] Xb = new double[satellite.Properties.X0.Length];
            double[] K1 = new double[satellite.Properties.X0.Length];
            double[] K2 = new double[satellite.Properties.X0.Length];
            double[] K3 = new double[satellite.Properties.X0.Length];
            double[] K4 = new double[satellite.Properties.X0.Length];

            // Create folder for the satellite
            string satelliteFolderPath = Path.Combine("SimulationDataFolder", "Satellites", $"Satellite_{SatelliteList.IndexOf(satellite) + 1}");
            Directory.CreateDirectory(satelliteFolderPath);

            string simulationDataFilePath = Path.Combine(satelliteFolderPath, "SimulationData.txt");
            using (StreamWriter writer = new StreamWriter(simulationDataFilePath))
            {
                writer.Write("Time" + "   " + "Position X" + "   " + "Position Y" + "   " + "Velocity X" + "   " + "Velocity Y" + "   " + "theta angle" + "   " + "beta angle");
                writer.WriteLine();
                for (int i = 0; i < X.Length; i++) X[i] = satellite.Properties.X0[i];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < X.Length; j++)
                    {
                        Xb[j] = X[j];
                        satellite.Properties.X0[j] = X[j];
                    }
                    totalForceCalculator.CalculateTotalForce();
                    K1 = Derivative(satellite);

                    // K1[0] = Velocity_x, K1[1] = Velocity_y, K1[2] = Acceleration_x, K1[3] = Acceleration_y

                    for (int j = 0; j < K1.Length; j++)
                    {
                        X[j] = Xb[j] + 0.5 * K1[j] * TimeStep;
                        satellite.Properties.X0[j] = X[j];
                    }
                    totalForceCalculator.CalculateTotalForce();
                    K2 = Derivative(satellite);
                    // K2[0] = Velocity_x, K2[1] = Velocity_y, K2[2] = Acceleration_x, K2[3] = Acceleration_y

                    for (int j = 0; j < K1.Length; j++)
                    {
                        X[j] = Xb[j] + 0.5 * K2[j] * TimeStep;
                        satellite.Properties.X0[j] = X[j];
                    }
                    totalForceCalculator.CalculateTotalForce();
                    K3 = Derivative(satellite);
                    // K3[0] = Velocity_x, K3[1] = Velocity_y, K3[2] = Acceleration_x, K3[3] = Acceleration_y

                    for (int j = 0; j < K1.Length; j++)
                    {
                        X[j] = Xb[j] + K3[j] * TimeStep;
                        satellite.Properties.X0[j] = X[j];
                    }
                    totalForceCalculator.CalculateTotalForce();
                    K4 = Derivative(satellite);
                    // K4[0] = Velocity_x, K4[1] = Velocity_y, K4[2] = Acceleration_x, K4[3] = Acceleration_y

                    for (int j = 0; j < X.Length; j++)
                    {
                        // X[0] = Position_x, X[1] = Position_y, X[2] = Velocity_x, X[3] = Velocity_y
                        // Xdot[0] = Velocity_x, Xdot[1] = Velocity_y, Xdot[2] = Acceleration_x, Xdot[3] = Acceleration_y
                        // X[0] = Xb[0] + (K1[0] + 2 * K2[0] + 2 * K3[0] + K4[0]) * TimeStep / 6;
                        // X[1] = Xb[1] + (K1[1] + 2 * K2[1] + 2 * K3[1] + K4[1]) * TimeStep / 6;
                        // X[2] = Xb[2] + (K1[2] + 2 * K2[2] + 2 * K3[2] + K4[2]) * TimeStep / 6;
                        // X[3] = Xb[3] + (K1[3] + 2 * K2[3] + 2 * K3[3] + K4[3]) * TimeStep / 6;

                        X[j] = Xb[j] + (K1[j] + 2 * K2[j] + 2 * K3[j] + K4[j]) * TimeStep / 6;
                    }

                    SatellitePositions[satellite].Add((X[0], X[1]));
                    SatelliteVelocity[satellite].Add((X[2], X[3]));
                    double thetaAngle = totalForceCalculator.GravityModel.ThetaAngle;
                    double betaAngle = totalForceCalculator.AerodynamicModel.BetaAngle;
                    dataWriter.WriteSimulationDataToFile(InitialTime, X, thetaAngle, betaAngle, writer);
                    InitialTime += TimeStep;
                    for (int j = 0; j < X.Length; j++) satellite.Properties.X0[j] = X[j];
                    totalForceCalculator.CalculateTotalForce(); // Recalculate total force after founding new state vector         
                }
                writer.Close();
            }
        }


        #endregion

        // This method works for checking satellite is in the field of ground station or not
        private void IsInFieldOfView(Satellite satellite, List<GroundStation> groundStations)
        {
            List<(double X, double Y)> positions = SatellitePositions[satellite];

            foreach (GroundStation groundStation in groundStations)
            {
                // Count = Time to be in Field of view
                int count = 0;

                foreach ((double X, double Y) position in positions)
                {
                    // Calculate the vector from the ground station to the satellite
                    double[] A = new double[2];
                    A[0] = position.X - groundStation.GroundStationLocation[0];
                    A[1] = position.Y - groundStation.GroundStationLocation[1];

                    // Calculate the magnitude of the vector from the ground station to the satellite
                    double A_Magnitude = Math.Sqrt(Math.Pow(A[0], 2) + Math.Pow(A[1], 2));

                    // Calculate the magnitude of the satellite's position vector
                    double satelliteDistance = Math.Sqrt(Math.Pow(position.X, 2) + Math.Pow(position.Y, 2));

                    // Calculate the dot product of the vectors
                    double dotProduct = position.X * groundStation.GroundStationLocation[0] + position.Y * groundStation.GroundStationLocation[1];

                    // Calculate the angle between the satellite's position vector and the line connecting the satellite to the ground station
                    double angle = Math.Acos(dotProduct / (satelliteDistance * A_Magnitude));

                    if (angle <= groundStation.FieldOfViewAngle)
                    {
                        count++;
                    }
                }

                if (count > 0)
                {
                    string message = $"{satellite.Properties.SatelliteName} is in the field of {groundStation.GroundStationName} for {count} times.";

                    SaveGroundStationReport(satellite, groundStation, message);
                    SaveSatelliteReport(satellite, groundStation, message);
                }
            }
        }

    }

    #endregion

    // This regions stores the classes related to drawing satellite and ground station for visualize 
    #region DrawToPicture

    // Abstract class for drawing satellite and ground station
    public abstract class DrawToPictureAbstract
    {
        // Satellite Properties
        protected PictureBox pictureBoxSatelliteViewer;
        protected PictureBox pictureBoxWorld;
        protected PictureBox pictureBoxSatellite;
        protected TextBox textBoxThetaAngle;
        protected TextBox textBoxPhiAngle;
        protected TextBox textBoxOrbitRadius;

        // Ground Station Properties 
        protected PictureBox pictureBoxGroundStationViewer;
        protected PictureBox pictureBoxWorld_2;
        protected PictureBox pictureBoxGroundStation;
        protected TextBox textBoxGroundStationTheta;
        protected TextBox textBoxGroundStationFoVAngle;

        public abstract void Draw_Radius(); 
        public abstract void Draw_CoordinateSystem();
        public abstract void CoordinateSystemConverter(object sender, EventArgs e); 
    }

    // Class for drawing Ground Station into a Picture Box
    public class DrawGroundStation : DrawToPictureAbstract
    {
        protected int width;
        protected int height;
        public Graphics graphics;
        public double GroundStationThetaAngle { get; set; } = 0; // Set default GroundStationThetaAngle 
        public double FieldOfViewAngle { get; set; } = 0; // Set default FieldOfViewAngle
        public double EarthRadius { get; private set; } = 6371000; // Radius of the earth
        public double PixelScale { get; private set; } = 40000; // Set default Pixel scale

        public DrawGroundStation(PictureBox pictureBoxGroundStationViewer, PictureBox pictureBoxWorld_2, PictureBox pictureBoxGroundStation,
                                 TextBox textBoxGroundStationTheta, TextBox textBoxGroundStationFoVAngle) 
        { 
            this.pictureBoxGroundStationViewer = pictureBoxGroundStationViewer;
            this.pictureBoxGroundStation = pictureBoxGroundStation;
            this.pictureBoxWorld_2 = pictureBoxWorld_2;
            this.textBoxGroundStationTheta = textBoxGroundStationTheta;
            this.textBoxGroundStationFoVAngle = textBoxGroundStationFoVAngle;
        }

        // Method to draw radius line (pictureBoxWorld_2)
        public override void Draw_Radius()
        {
            // Center of the pictureBox
            double x1 = pictureBoxWorld_2.Width / 2;
            double y1 = pictureBoxWorld_2.Height / 2;

            // Endpoint coordinates based on the GroundStationThetaAngle
            double x2 = x1 + (pictureBoxWorld_2.Width) * Math.Cos(GroundStationThetaAngle);
            double y2 = y1 - (pictureBoxWorld_2.Height) * Math.Sin(GroundStationThetaAngle); // Minus because screen coordinates

            // Draw the line
            graphics.DrawLine(Pens.Red, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        // Method to draw radius line (pictureBoxGroundStationViewer)
        public void Draw_RadiusCon()
        {
            // Endpoint coordinates based on the GroundStationThetaAngle
            double x2 = pictureBoxGroundStationViewer.Width * Math.Cos(GroundStationThetaAngle);
            double y2 = pictureBoxGroundStationViewer.Width * Math.Sin(GroundStationThetaAngle);

            // Draw the line
            graphics.DrawLine(Pens.Red, 0, 0, (float)x2, (float)-y2);
        }

        // Method to draw field view angle of the ground station (pictureBoxWorld_2)
        public void Draw_FieldOfView()
        {
            // Center of the pictureBox
            double x1 = pictureBoxWorld_2.Width / 2; // 2w
            double y1 = pictureBoxWorld_2.Height / 2; // 2h

            // Calculate x2 and y2 coordinates (start point coordinates)
            double x2 = x1 + (pictureBoxWorld_2.Width / 2) * Math.Cos(GroundStationThetaAngle);
            double y2 = y1 - (pictureBoxWorld_2.Height / 2) * Math.Sin(GroundStationThetaAngle); 

            // Half of the FoV angle
            double halfFOV = FieldOfViewAngle / 2;

            // Calculate endpoint coordinates for the left FOV line
            double xLeft = x2 + (pictureBoxWorld_2.Width) * Math.Cos(GroundStationThetaAngle - halfFOV);
            double yLeft = y2 - (pictureBoxWorld_2.Height) * Math.Sin(GroundStationThetaAngle - halfFOV);

            // Calculate endpoint coordinates for the right FOV line
            double xRight = x2 + (pictureBoxWorld_2.Width) * Math.Cos(GroundStationThetaAngle + halfFOV);
            double yRight = y2 - (pictureBoxWorld_2.Height) * Math.Sin(GroundStationThetaAngle + halfFOV);

            // Draw the lines
            graphics.DrawLine(Pens.White, (float)x2, (float)y2, (float)xLeft, (float)yLeft);
            graphics.DrawLine(Pens.White, (float)x2, (float)y2, (float)xRight, (float)yRight);
        }

        // Method to draw field view angle of the ground station (pictureBoxGroundStationViewer)
        public void Draw_FieldOfViewCon()
        {
            // Get the center of the pictureBox
            double centerX = pictureBoxWorld_2.Width / 2 * Math.Cos(-GroundStationThetaAngle);
            double centerY = pictureBoxWorld_2.Height/ 2 * Math.Sin(-GroundStationThetaAngle);
            // Calculate half of the FieldOfViewAngle
            double halfFOV = FieldOfViewAngle / 2;

            // Calculate endpoint coordinates for the left FOV line
            double xLeft = centerX + (pictureBoxWorld_2.Width / 2) * Math.Cos(GroundStationThetaAngle - halfFOV);
            double yLeft = centerY - (pictureBoxWorld_2.Height / 2) * Math.Sin(GroundStationThetaAngle - halfFOV);

            // Calculate endpoint coordinates for the right FOV line
            double xRight = centerX + (pictureBoxWorld_2.Width / 2) * Math.Cos(GroundStationThetaAngle + halfFOV);
            double yRight = centerY - (pictureBoxWorld_2.Height / 2) * Math.Sin(GroundStationThetaAngle + halfFOV);

            // Draw the lines
            graphics.DrawLine(Pens.White, (float)centerX, (float)centerY, (float)xLeft, (float)yLeft);
            graphics.DrawLine(Pens.White, (float)centerX, (float)centerY, (float)xRight, (float)yRight);
        }

        // Method to draw coordinate system and converting center of the pictureBoxGroundStationViewer as midpoint
        public override void Draw_CoordinateSystem()
        {
            // Find center of the pictureBoxGroundStationViewer
            int centerX = pictureBoxGroundStationViewer.Width / 2;
            int centerY = pictureBoxGroundStationViewer.Height / 2;
            int radius = Math.Min(centerX, centerY);

            // New coordinate system
            graphics.TranslateTransform(centerX, centerY);

            // Draw x-axis
            graphics.DrawLine(Pens.White, -radius, 0, radius, 0);

            // Draw y-axis
            graphics.DrawLine(Pens.White, 0, -radius, 0, radius);
        }

        // Method to fixing pictureBoxWorld_2 location 
        public void SetPictureBoxWorldPosition()
        {
            int WorldCenterX = (int)(pictureBoxGroundStationViewer.Location.X + pictureBoxGroundStationViewer.Width / 2 - pictureBoxWorld_2.Width / 2);
            int WorldCenterY = (int)(pictureBoxGroundStationViewer.Location.Y + pictureBoxGroundStationViewer.Height / 2 - pictureBoxWorld_2.Height / 2);
            pictureBoxWorld_2.Location = new Point(WorldCenterX, WorldCenterY);
        }

        // Method to changing pictureBoxGroundStation position wrt entered GroundStationThetaAngle
        public void SetPictureBoxGroundStationPosition()
        {
            double lineLength = EarthRadius / PixelScale;
            double x2 = lineLength * Math.Cos(GroundStationThetaAngle);
            double y2 = lineLength * Math.Sin(GroundStationThetaAngle);

            // Calculate the new X and Y location of the pictureBoxGroundStation
            int GroundStationCenterX = (int)(pictureBoxGroundStationViewer.Location.X + pictureBoxGroundStationViewer.Width / 2 - pictureBoxGroundStation.Width / 2 + x2);
            int GroundStationCenterY = (int)(pictureBoxGroundStationViewer.Location.Y + pictureBoxGroundStationViewer.Height / 2 - pictureBoxGroundStation.Height / 2 - y2);
            pictureBoxGroundStation.Location = new Point(GroundStationCenterX, GroundStationCenterY);
        }

        // Method to redraw the picture textBoxGroundStationTheta and textBoxGroundStationFoVAngle changes
        public override void CoordinateSystemConverter(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxGroundStationTheta.Text, out double newTheta))
            {
                GroundStationThetaAngle = newTheta * Math.PI / 180; // Convert degrees to 
                SetPictureBoxGroundStationPosition(); // Update the satellite position
                pictureBoxGroundStationViewer.Refresh(); // Trigger redraw to update the line
                pictureBoxWorld_2.Refresh();
            }
            if (double.TryParse(textBoxGroundStationFoVAngle.Text, out double newFoVAngle))
            {
                FieldOfViewAngle = newFoVAngle * Math.PI / 180; // Convert degrees to 
                SetPictureBoxGroundStationPosition(); // Update the satellite position
                pictureBoxGroundStationViewer.Refresh(); // Trigger redraw to update the line
                pictureBoxWorld_2.Refresh();
            }
        }
    }

    // Class for drawing Satellite into a Picture Box
    public class DrawSatellite : DrawToPictureAbstract
    {
        protected int width;
        protected int height;
        public Graphics graphics;
        public double ThetaAngle { get; set; } = 0; // Set default ThetaAngle 
        public double PhiAngle { get; set; } = 0; // Set default PhiAngle  
        public double OrbitRadius { get; private set; } = 11000000; // Set default OrbitRadius  
        public double PixelScale { get; private set; } = 127420; // Set default PixelScale  

        public DrawSatellite(PictureBox pictureBoxSatelliteViewer, PictureBox pictureBoxWorld, PictureBox pictureBoxSatellite,
                             TextBox textBoxTheta, TextBox textBoxPhi, TextBox textBoxOrbitRadius)
        {
            this.pictureBoxSatelliteViewer = pictureBoxSatelliteViewer;
            this.pictureBoxWorld = pictureBoxWorld;
            this.pictureBoxSatellite = pictureBoxSatellite;
            this.textBoxThetaAngle = textBoxTheta;
            this.textBoxPhiAngle = textBoxPhi;
            this.textBoxOrbitRadius = textBoxOrbitRadius;
        }

        // Methods to draw orbit radius of the satellite
        public void Draw_Orbit()
        {
            double lineLength = OrbitRadius / PixelScale;
            // Draw a dotted circle
            Pen dottedPen = new Pen(Color.White);
            dottedPen.DashPattern = new float[] { 1, 1 }; 

            // Draw the radius of the satellite
            graphics.DrawEllipse(dottedPen, (float)-lineLength, (float)-lineLength, (float)(2 * lineLength), (float)(2 * lineLength));
        }

        // Methods to draw tangent line (orbit) of the satellite
        public void Draw_Tangent()
        {
            double lineLength = OrbitRadius / PixelScale;
            double x2 = lineLength * Math.Cos(ThetaAngle);
            double y2 = lineLength * Math.Sin(ThetaAngle);

            // Calculate the perpendicular angle
            double perpTheta = ThetaAngle + Math.PI / 2; 

            // Calculate endpoint coordinates for the perpendicular line
            double x3 = pictureBoxSatelliteViewer.Width / 4 * Math.Cos(perpTheta);
            double y3 = pictureBoxSatelliteViewer.Width / 4 * Math.Sin(perpTheta);

            // Draw the perpendicular line
            Pen dottedPen = new Pen(Color.White);
            dottedPen.DashPattern = new float[] { 1, 1 }; 

            // Draw the tangent line of the satellite
            graphics.DrawLine(dottedPen, (float)x2, (float)-y2, (float)(x2 + x3), (float)(-y2 - y3));
        }

        // Method to draw radius line of the satellite wrt ThetaAngle
        public override void Draw_Radius()
        {
            // Convert orbit radius from meters to pixels
            double lineLength = OrbitRadius / PixelScale; 

            // Calculate endpoint coordinates based on ThetaAngle and orbit radius
            double x2 = lineLength * Math.Cos(ThetaAngle);
            double y2 = lineLength * Math.Sin(ThetaAngle);

            // Draw the line
            graphics.DrawLine(Pens.Red, 0, 0, (float)x2, (float)-y2);
        }

        // Method to draw coordinate system and converting center of the pictureBoxSatelliteViewer as midpoint
        public override void Draw_CoordinateSystem()
        {
            // Find center of the pictureBoxSatelliteViewer
            int centerX = pictureBoxSatelliteViewer.Width / 2;
            int centerY = pictureBoxSatelliteViewer.Height / 2;
            int radius = Math.Min(centerX, centerY);

            // New coordinate system
            graphics.TranslateTransform(centerX, centerY);

            // Draw x-axis
            graphics.DrawLine(Pens.White, -radius, 0, radius, 0);

            // Draw y-axis
            graphics.DrawLine(Pens.White, 0, -radius, 0, radius);
        }


        // Method to redraw the pictureBoxSatellite wrt textBoxThetaAngle and textBoxPhiAngle changes
        public override void CoordinateSystemConverter(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxThetaAngle.Text, out double newTheta))
            {
                ThetaAngle = newTheta * Math.PI / 180; // Convert degrees to 
                SetPictureBoxSatellitePosition(); // Update the satellite position
                pictureBoxSatelliteViewer.Refresh(); // Trigger redraw to update the line
            }
            if (double.TryParse(textBoxPhiAngle.Text, out double newPhi))
            {
                PhiAngle = newPhi * Math.PI / 180; // Convert degrees to
                SetPictureBoxSatellitePosition();
                pictureBoxSatelliteViewer.Refresh();
            }
        }

        // Method to draw velocity line of the satellite
        public void Draw_VelocityLine()
        {
            // Calculate start point coordinates based on ThetaAngle and orbit radius
            double lineLength = OrbitRadius / PixelScale;
            double x2 = lineLength * Math.Cos(ThetaAngle);
            double y2 = lineLength * Math.Sin(ThetaAngle);

            // Calculate the perpendicular angle
            double perpTheta = ThetaAngle + Math.PI / 2;
            // Calculate the Phi angle
            double fiAngle = perpTheta - PhiAngle; // Calculate the fiAngle

            // Calculate endpoint coordinates for the velocity line
            double x4 = pictureBoxSatelliteViewer.Width / 4 * Math.Cos(fiAngle);
            double y4 = pictureBoxSatelliteViewer.Width / 4 * Math.Sin(fiAngle);

            // Draw the velocity line
            graphics.DrawLine(Pens.White, (float)x2, (float)-y2, (float)(x2 + x4), (float)(-y2 - y4));
        }

        // Method to fixing pictureBoxWorld location 
        public void SetPictureBoxWorldPosition()
        {
            int WorldCenterX = (int)(pictureBoxSatelliteViewer.Location.X + pictureBoxSatelliteViewer.Width / 2 - pictureBoxWorld.Width / 2);
            int WorldCenterY = (int)(pictureBoxSatelliteViewer.Location.Y + pictureBoxSatelliteViewer.Height / 2 - pictureBoxWorld.Height / 2);
            pictureBoxWorld.Location = new Point(WorldCenterX, WorldCenterY);
        }

        // Method to changing location of the pictureBoxSatellite wrt ThetaAngle changes
        public void SetPictureBoxSatellitePosition()
        {
            double lineLength = OrbitRadius / PixelScale;
            double x2 = lineLength * Math.Cos(ThetaAngle);
            double y2 = lineLength * Math.Sin(ThetaAngle);

            int satelliteCenterX = (int)(pictureBoxSatelliteViewer.Location.X + pictureBoxSatelliteViewer.Width / 2 - pictureBoxSatellite.Width / 2 + x2);
            int satelliteCenterY = (int)(pictureBoxSatelliteViewer.Location.Y + pictureBoxSatelliteViewer.Height / 2 - pictureBoxSatellite.Height / 2 - y2);
            pictureBoxSatellite.Location = new Point(satelliteCenterX, satelliteCenterY);
        }


        /// <summary>
        ///  Method to Rescale picture wrt Orbit radius
        /// </summary>
        /// <param name="newOrbitRadius">Orbit radius of the satellite</param>
        /// <param name="originalWorldSize">Size of the pictureBoxWorld</param>
        /// <param name="originalSatelliteSize">Size of the pictureBoxSatellite</param>
        /// <param name="originalPixelScale">Pixel scale</param>
        public void RescalePicture(double newOrbitRadius, Size originalWorldSize, Size originalSatelliteSize, double originalPixelScale)
        {
            OrbitRadius = newOrbitRadius;
            // Update sizes and positions of pictureBoxWorld and pictureBoxSatellite based on orbitRadius
            if (OrbitRadius > 680000000)
            {
                // Change the sizes of pictureBoxWorld and pictureBoxSatellite
                pictureBoxWorld.Size = new Size(13, 13);
                pictureBoxSatellite.Size = new Size(2, 2);

                // Change pixelScale to 20000000 meters per pixel
                PixelScale = 20000000;
            }
            else if (OrbitRadius > 135000000)
            {
                // Change the sizes of pictureBoxWorld and pictureBoxSatellite
                pictureBoxWorld.Size = new Size(25, 25);
                pictureBoxSatellite.Size = new Size(4, 4);

                // Change pixelScale to 3000000 meters per pixel
                PixelScale = 3000000;
            }
            else if (OrbitRadius > 27500000)
            {
                // Change the sizes of pictureBoxWorld and pictureBoxSatellite
                pictureBoxWorld.Size = new Size(50, 50);
                pictureBoxSatellite.Size = new Size(8, 8);

                // Change pixelScale to 600000 meters per pixel
                PixelScale = 600000;
            }
            else if (OrbitRadius > 6371000)
            {
                // Change the sizes of pictureBoxWorld and pictureBoxSatellite
                pictureBoxWorld.Size = new Size(100, 100);
                pictureBoxSatellite.Size = new Size(15, 15);

                // Change pixelScale to 125000 meters per pixel
                PixelScale = 125000;
            }
            else
            {
                // Change the sizes of pictureBoxWorld and pictureBoxSatellite as default values
                pictureBoxWorld.Size = originalWorldSize;
                pictureBoxSatellite.Size = originalSatelliteSize;
                PixelScale = originalPixelScale;
            }
            SetPictureBoxWorldPosition();
            SetPictureBoxSatellitePosition();
            pictureBoxSatelliteViewer.Refresh(); // Redraw the picture
        }

    }

    #endregion

    // This region stores the classes related to drawing graph
    #region Graph Drawer
    // Abstract class for drawing graph of position and velocity
    public abstract class GraphDrawer
    {
        public abstract void WriteGraphName();
        public abstract void Draw_CoordinateSystem();
        public abstract void Draw_Border();
        public abstract void Draw_PositionZero();
    }

    // Class for drawing velocity trajectory of the satellite
    public class DrawTrajectoryVelocity : GraphDrawer
    {
        public Graphics graphics;
        public Simulation simulation;
        protected PictureBox pictureBoxSatelliteVelocityTrajectoryViewer;

        public DrawTrajectoryVelocity(PictureBox pictureBoxSatelliteVelocityTrajectoryViewer)
        {
            this.pictureBoxSatelliteVelocityTrajectoryViewer = pictureBoxSatelliteVelocityTrajectoryViewer;

        }

        // Method to draw the trajectory based on the given list of velocity points
        public void Draw_Trajectory(List<(double X, double Y)> velocity)
        {
            if (velocity.Count == 0)
                return;

            PointF[] velocityPoints = new PointF[velocity.Count];

            double xMaxAbs = double.MinValue;
            double yMaxAbs = double.MinValue;
            double xMaxValue = double.MinValue;
            double xMinValue = double.MaxValue;
            double yMinValue = double.MaxValue;
            double yMaxValue = double.MinValue;

            // Determine the maximum and minimum points to scale the graph
            foreach (var point in velocity)
            {
                if (Math.Abs(point.X) > xMaxAbs)
                    xMaxAbs = Math.Abs(point.X);

                if (Math.Abs(point.Y) > yMaxAbs)
                    yMaxAbs = Math.Abs(point.Y);

                if (point.X > xMaxValue)
                    xMaxValue = point.X;

                if (point.X < xMinValue)
                    xMinValue = point.X;

                if (point.Y < yMinValue)
                    yMinValue = point.Y;

                if (point.Y > yMaxValue)
                    yMaxValue = point.Y;
            }

            // Calculate the scale factors of x and y based on the pictureBoxSatelliteVelocityTrajectoryViewer size
            double dScaleX = (pictureBoxSatelliteVelocityTrajectoryViewer.Width - 200) / (2 * xMaxAbs);
            double dScaleY = (pictureBoxSatelliteVelocityTrajectoryViewer.Height - 200) / (2 * yMaxAbs);

            // Convert velocity points to calculated pixel points
            for (int i = 0; i < velocity.Count; i++)
            {
                int pixelX = (int)(velocity[i].X * dScaleX) + pictureBoxSatelliteVelocityTrajectoryViewer.Width / 2;
                int pixelY = pictureBoxSatelliteVelocityTrajectoryViewer.Height / 2 - (int)(velocity[i].Y * dScaleY);
                velocityPoints[i] = new PointF(pixelX, pixelY);
            }

            // Draw each point on the velocity trajectory
            foreach (var point in velocityPoints)
            {
                graphics.FillEllipse(Brushes.Red, point.X - 2, point.Y - 2, 4, 4);
            }

            // Border positions
            int bottomBorderY = pictureBoxSatelliteVelocityTrajectoryViewer.Height - 30;
            int leftBorderX = 30;

            // Draw little lines and labels for Xmax, Xmin, Ymin, Ymax values of the simulation result
            // Xmax point
            int xMaxPixelX = (int)(xMaxValue * dScaleX) + pictureBoxSatelliteVelocityTrajectoryViewer.Width / 2;
            graphics.DrawLine(Pens.Red, xMaxPixelX, bottomBorderY, xMaxPixelX, bottomBorderY + 5);
            string xMaxLabel = xMaxValue.ToString("F2");
            Font labelFont = new Font("Arial", 10);
            SizeF labelSize = graphics.MeasureString(xMaxLabel, labelFont);
            graphics.DrawString(xMaxLabel, labelFont, Brushes.Black, xMaxPixelX - labelSize.Width / 2, bottomBorderY + 9);

            // Xmin point
            int xMinPixelX = (int)(xMinValue * dScaleX) + pictureBoxSatelliteVelocityTrajectoryViewer.Width / 2;
            graphics.DrawLine(Pens.Red, xMinPixelX, bottomBorderY, xMinPixelX, bottomBorderY + 5);
            string xMinLabel = xMinValue.ToString("F2");
            SizeF xMinLabelSize = graphics.MeasureString(xMinLabel, labelFont);
            graphics.DrawString(xMinLabel, labelFont, Brushes.Black, xMinPixelX - xMinLabelSize.Width / 2, bottomBorderY + 9);

            // Ymin point
            int yMinPixelY = pictureBoxSatelliteVelocityTrajectoryViewer.Height / 2 - (int)(yMinValue * dScaleY);
            graphics.DrawLine(Pens.Red, leftBorderX, yMinPixelY, leftBorderX - 5, yMinPixelY);
            string yMinLabel = yMinValue.ToString("F2");
            SizeF yMinLabelSize = graphics.MeasureString(yMinLabel, labelFont);
            graphics.RotateTransform(-90);
            graphics.DrawString(yMinLabel, labelFont, Brushes.Black, -yMinPixelY - yMinLabelSize.Height * 2, leftBorderX - 20);
            graphics.ResetTransform();

            // Ymax point
            int yMaxPixelY = pictureBoxSatelliteVelocityTrajectoryViewer.Height / 2 - (int)(yMaxValue * dScaleY);
            graphics.DrawLine(Pens.Red, leftBorderX, yMaxPixelY, leftBorderX - 5, yMaxPixelY);
            string yMaxLabel = yMaxValue.ToString("F2");
            SizeF yMaxLabelSize = graphics.MeasureString(yMaxLabel, labelFont);
            graphics.RotateTransform(-90);
            graphics.DrawString(yMaxLabel, labelFont, Brushes.Black, -yMaxPixelY - yMaxLabelSize.Height * 2, leftBorderX - 20);
            graphics.ResetTransform();
        }

        // Method to write the graph name on the pictureBoxSatelliteVelocityTrajectoryViewer
        public override void WriteGraphName()
        {
            // Font for the graph name
            Font graphNameFont = new Font("Arial", 12, FontStyle.Bold);
            Brush graphNameBrush = Brushes.Black;

            int width = pictureBoxSatelliteVelocityTrajectoryViewer.Width;

            // Calculate the position for writing the graph name
            PointF graphNamePosition = new PointF((float)(width / 2 - 150), 10);

            // Write the graph name
            graphics.DrawString("Satellite Velocity (m/s) with Time (s) Graph", graphNameFont, graphNameBrush, graphNamePosition);
        }

        // Method to draw the coordinate system on the pictureBoxSatelliteVelocityTrajectoryViewer
        public override void Draw_CoordinateSystem()
        {
            int width = pictureBoxSatelliteVelocityTrajectoryViewer.Width;
            int height = pictureBoxSatelliteVelocityTrajectoryViewer.Height;

            Pen dottedPen = new Pen(Color.Black);
            dottedPen.DashPattern = new float[] { 1f, 1f };

            // Draw x-axis (from left border to right border)
            graphics.DrawLine(dottedPen, 30, height / 2, width - 30, height / 2);

            // Draw y-axis (from top border to bottom border)
            graphics.DrawLine(dottedPen, width / 2, 30, width / 2, height - 30);
        }

        // Method to draw a border around the pictureBoxSatelliteVelocityTrajectoryViewer
        public override void Draw_Border()
        {
            Pen borderPen = new Pen(Color.Black, 2);

            int width = pictureBoxSatelliteVelocityTrajectoryViewer.Width;
            int height = pictureBoxSatelliteVelocityTrajectoryViewer.Height;

            graphics.DrawLine(borderPen, 30, 30, width - 30, 30); // Top line
            graphics.DrawLine(borderPen, 30, 30, 30, height - 30); // Left line
            graphics.DrawLine(borderPen, width - 30, 30, width - 30, height - 30); // Right line
            graphics.DrawLine(borderPen, 30, height - 30, width - 30, height - 30); // Bottom line
        }

        // Method to draw position zero ticks and labels on the coordinate system
        public override void Draw_PositionZero()
        {
            // Pen and label font settings
            Pen tickPen = new Pen(Color.Red, 1);
            Font labelFont = new Font("Arial", 8);
            Brush labelBrush = Brushes.Black;
            int tickLength = 5;

            int width = pictureBoxSatelliteVelocityTrajectoryViewer.Width;
            int height = pictureBoxSatelliteVelocityTrajectoryViewer.Height;

            // Calculate the center of the pictureBoxSatelliteVelocityTrajectoryViewer
            int centerX = width / 2;
            int centerY = height / 2;

            // Draw the y-axis tick and label at x=0
            if (centerY >= 30 && centerY <= height - 30)
            {
                graphics.DrawLine(tickPen, 30, centerY, 30 + tickLength, centerY);
                graphics.DrawString("0", labelFont, labelBrush, 30 - tickLength - 10, centerY - 5);
            }

            // Draw the x-axis tick and label at y=0
            if (centerX >= 30 && centerX <= width - 30)
            {
                graphics.DrawLine(tickPen, centerX, height - 30, centerX, height - 30 - tickLength);
                graphics.DrawString("0", labelFont, labelBrush, centerX - 5, height - 30 + tickLength + 2);
            }
        }
    }

    // Class for drawing position trajectory of the satellite
    public class DrawTrajectoryPosition : GraphDrawer
    {
        public Graphics graphics;
        public Simulation simulation;
        protected PictureBox pictureBoxSatelliteTrajectoryViewer;

        public DrawTrajectoryPosition(PictureBox pictureBoxSatelliteTrajectoryViewer)
        {
            this.pictureBoxSatelliteTrajectoryViewer = pictureBoxSatelliteTrajectoryViewer;
        }

        // Method to draw the coordinate system on the pictureBoxSatelliteTrajectoryViewer
        public override void Draw_CoordinateSystem()
        {
            int width = pictureBoxSatelliteTrajectoryViewer.Width;
            int height = pictureBoxSatelliteTrajectoryViewer.Height;

            Pen dottedPen = new Pen(Color.Black);
            dottedPen.DashPattern = new float[] { 1f, 1f };

            // Draw x-axis (from left border to right border)
            graphics.DrawLine(dottedPen, 30, height / 2, width - 30, height / 2);

            // Draw y-axis (from top border to bottom border)
            graphics.DrawLine(dottedPen, width / 2, 30, width / 2, height - 30);
        }

        // Method to draw a border around the pictureBoxSatelliteTrajectoryViewer
        public override void Draw_Border()
        {
            Pen borderPen = new Pen(Color.Black, 2);

            int width = pictureBoxSatelliteTrajectoryViewer.Width;
            int height = pictureBoxSatelliteTrajectoryViewer.Height;

            graphics.DrawLine(borderPen, 30, 30, width - 30, 30); // Top line
            graphics.DrawLine(borderPen, 30, 30, 30, height - 30); // Left line
            graphics.DrawLine(borderPen, width - 30, 30, width - 30, height - 30); // Right line
            graphics.DrawLine(borderPen, 30, height - 30, width - 30, height - 30); // Bottom line
        }

        // Method to write the graph name on the pictureBoxSatelliteTrajectoryViewer
        public override void WriteGraphName()
        {
            // Font for the graph name
            Font graphNameFont = new Font("Arial", 12, FontStyle.Bold);
            Brush graphNameBrush = Brushes.Black;

            int width = pictureBoxSatelliteTrajectoryViewer.Width;

            // Calculate the position for writing the graph name
            PointF graphNamePosition = new PointF((float)(width / 2 - 145), 10);

            // Write the graph name
            graphics.DrawString("Satellite Position (m) with Time (s) Graph", graphNameFont, graphNameBrush, graphNamePosition);
        }

        // Method to draw position zero ticks and labels on the coordinate system
        public override void Draw_PositionZero()
        {
            // Pen and label font settings
            Pen tickPen = new Pen(Color.Red, 1);
            Font labelFont = new Font("Arial", 8);
            Brush labelBrush = Brushes.Black;
            int tickLength = 5;

            // Get the dimensions of the PictureBox
            int width = pictureBoxSatelliteTrajectoryViewer.Width;
            int height = pictureBoxSatelliteTrajectoryViewer.Height;

            // Calculate the center of the pictureBoxSatelliteTrajectoryViewer
            int centerX = width / 2;
            int centerY = height / 2;

            // Draw the y-axis tick and label at x=0
            if (centerY >= 30 && centerY <= height - 30)
            {
                graphics.DrawLine(tickPen, 30, centerY, 30 + tickLength, centerY);
                graphics.DrawString("0", labelFont, labelBrush, 30 - tickLength - 10, centerY - 5);
            }

            // Draw the x-axis tick and label at y=0
            if (centerX >= 30 && centerX <= width - 30)
            {
                graphics.DrawLine(tickPen, centerX, height - 30, centerX, height - 30 - tickLength);
                graphics.DrawString("0", labelFont, labelBrush, centerX - 5, height - 30 + tickLength + 2);
            }
        }

        // Method to draw the trajectory based on the given list of position points
        public void Draw_Trajectory(List<(double X, double Y)> positions)
        {
            if (positions.Count == 0)
                return;

            PointF[] positionPoints = new PointF[positions.Count];

            double xMaxAbs = double.MinValue;
            double yMaxAbs = double.MinValue;
            double xMaxValue = double.MinValue;
            double xMinValue = double.MaxValue;
            double yMinValue = double.MaxValue;
            double yMaxValue = double.MinValue;

            // Determine the maximum and minimum points to scale the graph
            foreach (var point in positions)
            {
                if (Math.Abs(point.X) > xMaxAbs)
                    xMaxAbs = Math.Abs(point.X);

                if (Math.Abs(point.Y) > yMaxAbs)
                    yMaxAbs = Math.Abs(point.Y);

                if (point.X > xMaxValue)
                    xMaxValue = point.X;

                if (point.X < xMinValue)
                    xMinValue = point.X;

                if (point.Y < yMinValue)
                    yMinValue = point.Y;

                if (point.Y > yMaxValue)
                    yMaxValue = point.Y;
            }
            // Convert position points to pixel points
            double dScaleX = (pictureBoxSatelliteTrajectoryViewer.Width - 200) / (2 * xMaxAbs); // Decrease 20 from each side for margin
            double dScaleY = (pictureBoxSatelliteTrajectoryViewer.Height - 200) / (2 * yMaxAbs); // Decrease 20 from each side for margin

            // Convert position points to calculated pixel points
            for (int i = 0; i < positions.Count; i++)
            {
                int pixelX = (int)(positions[i].X * dScaleX) + pictureBoxSatelliteTrajectoryViewer.Width / 2;
                int pixelY = pictureBoxSatelliteTrajectoryViewer.Height / 2 - (int)(positions[i].Y * dScaleY);
                positionPoints[i] = new PointF(pixelX, pixelY);
            }

            // Draw each point on the velocity trajectory
            foreach (var point in positionPoints)
            {
                graphics.FillEllipse(Brushes.Red, point.X - 2, point.Y - 2, 4, 4);
            }

            // Border positions
            int bottomBorderY = pictureBoxSatelliteTrajectoryViewer.Height - 30;
            int leftBorderX = 30;

            // Draw little lines and labels for Xmax, Xmin, Ymin, Ymax values of the simulation result
            // Xmax point
            int xMaxPixelX = (int)(xMaxValue * dScaleX) + pictureBoxSatelliteTrajectoryViewer.Width / 2;
            graphics.DrawLine(Pens.Red, xMaxPixelX, bottomBorderY, xMaxPixelX, bottomBorderY + 5);
            string xMaxLabel = xMaxValue.ToString("F2");
            Font labelFont = new Font("Arial", 10);
            SizeF labelSize = graphics.MeasureString(xMaxLabel, labelFont);
            graphics.DrawString(xMaxLabel, labelFont, Brushes.Black, xMaxPixelX - labelSize.Width / 2, bottomBorderY + 9);

            // Xmin point
            int xMinPixelX = (int)(xMinValue * dScaleX) + pictureBoxSatelliteTrajectoryViewer.Width / 2;
            graphics.DrawLine(Pens.Red, xMinPixelX, bottomBorderY, xMinPixelX, bottomBorderY + 5);
            string xMinLabel = xMinValue.ToString("F2");
            SizeF xMinLabelSize = graphics.MeasureString(xMinLabel, labelFont);
            graphics.DrawString(xMinLabel, labelFont, Brushes.Black, xMinPixelX - xMinLabelSize.Width / 2, bottomBorderY + 9);

            // Ymin point
            int yMinPixelY = pictureBoxSatelliteTrajectoryViewer.Height / 2 - (int)(yMinValue * dScaleY);
            graphics.DrawLine(Pens.Red, leftBorderX, yMinPixelY, leftBorderX - 5, yMinPixelY);
            string yMinLabel = yMinValue.ToString("F2");
            SizeF yMinLabelSize = graphics.MeasureString(yMinLabel, labelFont);
            graphics.RotateTransform(-90);
            graphics.DrawString(yMinLabel, labelFont, Brushes.Black, -yMinPixelY - yMinLabelSize.Height * 2, leftBorderX - 20);
            graphics.ResetTransform();

            // Ymax point
            int yMaxPixelY = pictureBoxSatelliteTrajectoryViewer.Height / 2 - (int)(yMaxValue * dScaleY);
            graphics.DrawLine(Pens.Red, leftBorderX, yMaxPixelY, leftBorderX - 5, yMaxPixelY);
            string yMaxLabel = yMaxValue.ToString("F2");
            SizeF yMaxLabelSize = graphics.MeasureString(yMaxLabel, labelFont);
            graphics.RotateTransform(-90);
            graphics.DrawString(yMaxLabel, labelFont, Brushes.Black, -yMaxPixelY - yMaxLabelSize.Height * 2, leftBorderX - 20);
            graphics.ResetTransform();
        }

    }

    #endregion

    // This region stores the classes related to space model
    #region Space Models

    // Abstract class for SpaceModels of Gravity Model and Aerodynamic Model
    public abstract class SpaceModels
    {
        // Constant values
        protected const double EarthRadius = 6371000;   // Earth's radius in meters
        protected const double GravityConstant = 9.81;  // Gravitational constant in m/s^2
        protected const double AirDensity = 1e-10;      // Air density in kg/m^3 
        protected const double CoefficientOfDrag = 0.3; // Coefficient of drag for aerodynamic model
        public double ThetaAngle { get; protected set; } // Angle for gravity model
        public double BetaAngle { get; protected set; }  // Angle for aerodynamic model
        public abstract void CalculateForceComponents();
        public abstract double CalculateForce();
    }

    // Class for calculating gravity model of the satellite
    public class GravityModel : SpaceModels
    {
        public double GravitationalForce_XComponent { get; private set; } // X-component of gravitational force
        public double GravitationalForce_YComponent { get; private set; } // Y-component of gravitational force

        public Satellite satellite;

        public GravityModel(Satellite satellite)
        {
            this.satellite = satellite;
            CalculateForceComponents();
        }

        // Method to calculate gravity with respect to height
        private double CalculateGravity()
        {
            double height = CalculateHeight();
            return GravityConstant * Math.Pow((EarthRadius / (EarthRadius + height)), 2);
        }

        // Method to calculate height
        private double CalculateHeight()
        {
            return Math.Sqrt(Math.Pow(satellite.Properties.X0[0], 2) + Math.Pow(satellite.Properties.X0[1], 2)) - EarthRadius;
        }


        // Method to calculate theta angle
        private double CalculateThetaAngle()
        {
            ThetaAngle = Math.Atan2(satellite.Properties.X0[1], satellite.Properties.X0[0]);
            return ThetaAngle;
        }
        // Method to calculate gravitational force
        public override double CalculateForce()
        {
            double gravity = CalculateGravity();
            return satellite.Properties.SatelliteMass * gravity; // N
        }
        // Method to calculate gravitational force components
        public override void CalculateForceComponents()
        {
            double GravityForce = CalculateForce(); // Gravity at given position
            double thetaAngle = CalculateThetaAngle(); // Calculate theta angle

            // Calculate gravitational force components
            GravitationalForce_XComponent = -GravityForce * Math.Cos(thetaAngle); // N
            GravitationalForce_YComponent = -GravityForce * Math.Sin(thetaAngle); // N
        }
    }

    // Class for calculating aerodynamic model of the satellite
    public class AerodynamicModel : SpaceModels
    {
        public double AerodynamicForce_XComponent { get; private set; } // X-component of aerodynamic force
        public double AerodynamicForce_YComponent { get; private set; } // Y-component of aerodynamic force

        public Satellite satellite;
        public AerodynamicModel(Satellite satellite)
        {
            this.satellite = satellite;
            CalculateForceComponents();
        }

        // Method to calculate satellite velocity
        private double CalculateVelocity()
        {
            return Math.Sqrt(Math.Pow(satellite.Properties.X0[2], 2) + Math.Pow(satellite.Properties.X0[3], 2));
        }

        // Method to calculate beta angle
        public double CalculateBetaAngle()
        {
            BetaAngle = Math.Atan2(satellite.Properties.X0[3], satellite.Properties.X0[2]);
            return BetaAngle;
        }

        // Method to calculate aerodynamic force
        public override double CalculateForce()
        {
            double Velocity = CalculateVelocity();
            return 0.5 * AirDensity * Math.Pow(Velocity, 2) * CoefficientOfDrag; // N
        }

        // Method to calculate aerodynamic force components
        public override void CalculateForceComponents()
        {
            double aerodynamicForce = CalculateForce();
            double betaAngle = CalculateBetaAngle();
            AerodynamicForce_XComponent = -aerodynamicForce * Math.Sin(betaAngle); // N 
            AerodynamicForce_YComponent = -aerodynamicForce * Math.Cos(betaAngle); // N
        }
    }

    // Class for calculating the total force of the satellite's x and y components.
    public class TotalForceCalculator : SpaceModels
    {
        public double TotalForce_XComponent { get; private set; } // Total force of the satellite's X-component
        public double TotalForce_YComponent { get; private set; } // Total force of the satellite's Y-component

        private readonly GravityModel gravityModel;
        private readonly AerodynamicModel aerodynamicModel;
        public GravityModel GravityModel => gravityModel;
        public AerodynamicModel AerodynamicModel => aerodynamicModel;

        public TotalForceCalculator(GravityModel gravityModel, AerodynamicModel aerodynamicModel)
        {
            this.gravityModel = gravityModel;
            this.aerodynamicModel = aerodynamicModel;
            CalculateForceComponents();

        }

        // Method to calculate total force components of the satellite
        public void CalculateTotalForce()
        {
            GravityModel.CalculateForceComponents();
            AerodynamicModel.CalculateForceComponents();
            TotalForce_XComponent = gravityModel.GravitationalForce_XComponent + aerodynamicModel.AerodynamicForce_XComponent;
            TotalForce_YComponent = gravityModel.GravitationalForce_YComponent + aerodynamicModel.AerodynamicForce_YComponent;
        }

        // Method to calculate the total force magnitude
        public override double CalculateForce()
        {
            return Math.Sqrt(Math.Pow(TotalForce_XComponent, 2) + Math.Pow(TotalForce_YComponent, 2));
        }

        // Method to calculate total force components
        public override void CalculateForceComponents()
        {
            CalculateTotalForce();
        }
    }

    #endregion

    // This region stores the classes related to Ground Station
    #region Ground Station

    public enum GroundStationType
    {
        C_GroundStation,  // Ground stations that only communicate with the satellites
        T_GroundStation,  // Ground stations that only track the satellites
        BCT_GroundStation  // Ground stations that both communicate with and track the satellites
    }

    // Abstract class for different types of ground stations
    public abstract class GroundStation
    {
        public double EarthRadius = 6371000; // Earth's radius in meters
        public double ThetaAngle { get; set; } // Theta angle of the ground station
        public double FieldOfViewAngle { get; set; } // Field of view angle of the ground station 
        public string GroundStationName {  get; set; } // Ground station name
        public double[] GroundStationLocation {  get; set; } // GroundStationLocation[0] = x , GroundStationLocation[1] = y
        public GroundStationType GroundStationType { get; set; } // Type of the ground station
        public abstract void GenerateName();
        public abstract void CalculateLocation();

        // Method to convert degrees to radians
        protected double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }

    // Class representing a ground station that only communicates with satellites
    public class C_GroundStation : GroundStation
    {
        private static Random rand = new Random();

        public C_GroundStation(GroundStationType groundStationType, double theta, double fieldOfViewAngle)
        {
            groundStationType = groundStationType;
            ThetaAngle = theta;
            FieldOfViewAngle = fieldOfViewAngle;
            GroundStationLocation = new double[2];
            GenerateName();
            CalculateLocation();
        }

        // Method to generate a name for the ground station
        public override void GenerateName()
        {
            int randomNumber = rand.Next(1000, 9999); // Generate a random 4-digit number
            GroundStationName = "CGS_" + randomNumber.ToString();
        }

        // Method to calculate the location of the ground station
        public override void CalculateLocation()
        {
            double thetaRadians = ToRadians(ThetaAngle);
            GroundStationLocation[0] = EarthRadius * Math.Cos(thetaRadians);
            GroundStationLocation[1] = EarthRadius * Math.Sin(thetaRadians);
        }

    }

    // Class representing a ground station that only tracks satellites
    public class T_GroundStation : GroundStation
    {
        private static Random rand = new Random();

        public T_GroundStation(GroundStationType groundStationType, double theta, double fieldOfViewAngle)
        {
            groundStationType = groundStationType;
            ThetaAngle = theta;
            FieldOfViewAngle = fieldOfViewAngle;
            GroundStationLocation = new double[2];

            GenerateName();
            CalculateLocation();
        }

        // Method to generate a name for the ground station
        public override void GenerateName()
        {
            int randomNumber = rand.Next(1000, 9999); // Generate a random 4-digit number
            GroundStationName = "TGS_" + randomNumber.ToString();
        }

        // Method to calculate the location of the ground station
        public override void CalculateLocation()
        {
            double thetaRadians = ToRadians(ThetaAngle);
            GroundStationLocation[0] = EarthRadius * Math.Cos(thetaRadians);
            GroundStationLocation[1] = EarthRadius * Math.Sin(thetaRadians);
        }

    }

    // Class representing a ground station that both communicates with and tracks satellites
    public class BCT_GroundStation : GroundStation
    {
        private static Random rand = new Random();

        public BCT_GroundStation(GroundStationType groundStationType, double theta, double fieldOfViewAngle)
        {
            groundStationType = groundStationType;
            ThetaAngle = theta;
            FieldOfViewAngle = fieldOfViewAngle;
            GroundStationLocation = new double[2];

            GenerateName();
            CalculateLocation();
        }

        // Method to generate a name for the ground station
        public override void GenerateName()
        {
            int randomNumber = rand.Next(1000, 9999); // Generate a random 4-digit number
            GroundStationName = "BGS_" + randomNumber.ToString();
        }

        // Method to calculate the location of the ground station
        public override void CalculateLocation()
        {
            double thetaRadians = ToRadians(ThetaAngle);
            GroundStationLocation[0] = EarthRadius * Math.Cos(thetaRadians);
            GroundStationLocation[1] = EarthRadius * Math.Sin(thetaRadians);
        }

    }

    #endregion

    // This region stores the classes related to Satellite
    #region Satellite 

    public enum SatelliteType
    {
        ReceiverSatellite, // Satellites that only receive data from the ground stations
        SenderSatellite    // Satellites that only send data to the ground stations
    }

    // Abstract class for different types of satellites
    public abstract class Satellite
    {
        // Properties of the satellite
        public SatelliteProperties Properties { get; set; }

        // Method to generate a name for the satellite
        public virtual void GenerateName()
        {
            Properties.SatelliteName = "Satellite_" + new Random().Next(1000, 9999);
        }
        public abstract void CalculateComponents();

        protected double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }

    // Class that store the properties of the satellite
    public class SatelliteProperties
    {
        public string SatelliteName { get; set; }
        public SatelliteType SatelliteType { get; set; }
        public double InitialOrbitRadius { get; set; } // m
        public double InitialThetaAngle { get; set; }
        public double InitialPhiAngle { get; set; }
        public double SatelliteMass { get; set; } // kg
        public double SatelliteInitialVelocity { get; set; } // m/s
        public double[] X0 { get; set; } // X0[0] = Position_x, X0[1] = Position_y, X0[2] = Velocity_x, X0[3] = Velocity_y
        public double[] Xdot { get; set; } // Xdot[0] = Velocity_x, Xdot[1] = Velocity_y, Xdot[2] = Acceleration_x, Xdot[3] = Acceleration_y
    }

    // Class representing a satellite that only receive data from the ground stations
    public class ReceiverSatellite : Satellite
    {
        private static Random rand = new Random();

        public ReceiverSatellite(SatelliteType satelliteType, double radius, double theta, double phi, double mass, double velocity)
        {
            Properties = new SatelliteProperties
            {
                SatelliteType = satelliteType,
                InitialOrbitRadius = radius,
                InitialThetaAngle = theta,
                InitialPhiAngle = phi,
                SatelliteMass = mass,
                SatelliteInitialVelocity = velocity,
                X0 = new double[4],
                Xdot = new double[4],
            };

            GenerateName();
            CalculateComponents();
        }

        // Method to generate a name for the receiver satellite
        public override void GenerateName()
        {
            int randomNumber = rand.Next(1000, 9999); // Generate a random 4-digit number
            Properties.SatelliteName = "RS_" + randomNumber.ToString();
        }

        // Method to calculates satellite components
        public override void CalculateComponents()
        {
            double thetaRadians = ToRadians(Properties.InitialThetaAngle);
            double fiRadians = ToRadians(Properties.InitialPhiAngle);

            // Calculate components of the satellite
            Properties.X0[0] = Properties.InitialOrbitRadius * Math.Cos(thetaRadians); // Initial value for Position_x
            Properties.X0[1] = Properties.InitialOrbitRadius * Math.Sin(thetaRadians); // Initial value for Position_y
            Properties.X0[2] = Properties.SatelliteInitialVelocity * Math.Sin(thetaRadians - fiRadians); // Initial value for Velocity_x
            Properties.X0[3] = Properties.SatelliteInitialVelocity * Math.Cos(thetaRadians - fiRadians); // Initial value for Velocity_y
        }
    }

    // Class representing a satellite that only send data to the ground stations
    public class SenderSatellite : Satellite
    {
        private static Random rand = new Random();

        public SenderSatellite(SatelliteType satelliteType, double radius, double theta, double phi, double mass, double velocity)
        {
            Properties = new SatelliteProperties
            {
                SatelliteType = satelliteType,
                InitialOrbitRadius = radius,
                InitialThetaAngle = theta,
                InitialPhiAngle = phi,
                SatelliteMass = mass,
                SatelliteInitialVelocity = velocity,
                X0 = new double[4],
                Xdot = new double[4],
            };

            GenerateName();
            CalculateComponents();
        }

        // Method to generate a name for the sender satellite
        public override void GenerateName()
        {
            int randomNumber = rand.Next(1000, 9999); // Generate a random 4-digit number
            Properties.SatelliteName = "SS_" + randomNumber.ToString();
        }

        // Method to calculates satellite components
        public override void CalculateComponents()
        {
            double thetaRadians = ToRadians(Properties.InitialThetaAngle);
            double fiRadians = ToRadians(Properties.InitialPhiAngle);

            // Calculate components of the satellite
            Properties.X0[0] = Properties.InitialOrbitRadius * Math.Cos(thetaRadians); // Initial value for Position_x
            Properties.X0[1] = Properties.InitialOrbitRadius * Math.Sin(thetaRadians); // Initial value for Position_y
            Properties.X0[2] = Properties.SatelliteInitialVelocity * Math.Sin(thetaRadians - fiRadians); // Initial value for Velocity_x
            Properties.X0[3] = Properties.SatelliteInitialVelocity * Math.Cos(thetaRadians - fiRadians); // Initial value for Velocity_y
        }
    }

    #endregion

    // This region stores the class of the DataWriter
    #region Data Writer

    public class DataWriter
    {

        // Method to write satellite data to a file
        public void WriteSatelliteDataToFile(Satellite satellite, string satelliteFolderPath)
        {
            // Create a new directory if it doesn't exist
            if (!Directory.Exists(satelliteFolderPath))
            {
                Directory.CreateDirectory(satelliteFolderPath);
            }
            // Define file path
            string satelliteInfoFilePath = Path.Combine(satelliteFolderPath, "SatelliteInfo.txt");

            // Write satellite data to the file
            using (StreamWriter writer = new StreamWriter(satelliteInfoFilePath))
            {
                writer.WriteLine($"Satellite Type: {satellite.Properties.SatelliteType}");
                writer.WriteLine($"Satellite Name: {satellite.Properties.SatelliteName}");
                writer.WriteLine($"Initial Orbit Radius: {satellite.Properties.InitialOrbitRadius}");
                writer.WriteLine($"Initial Theta Angle: {satellite.Properties.InitialThetaAngle}");
                writer.WriteLine($"Initial Phi Angle: {satellite.Properties.InitialPhiAngle}");
                writer.WriteLine($"Satellite Mass: {satellite.Properties.SatelliteMass}");
                writer.WriteLine($"Satellite Initial Velocity: {satellite.Properties.SatelliteInitialVelocity}");
            }
        }

        // Method to write ground station data to a file
        public void WriteGroundStationDataToFile (GroundStation groundStation, string groundStationFolderPath)
        {
            // Create a new directory if it doesn't exist
            if (!Directory.Exists(groundStationFolderPath))
            {
                Directory.CreateDirectory(groundStationFolderPath);
            }
            // Define file path
            string groundStationInfoFilePath = Path.Combine(groundStationFolderPath, "GroundStationInfo.txt");

            // Write ground station data to the file
            using (StreamWriter writer = new StreamWriter(groundStationInfoFilePath))
            {
                writer.WriteLine($"Ground Station Type: {groundStation.GroundStationType}");
                writer.WriteLine($"Ground Station Name: {groundStation.GroundStationName}");
                writer.WriteLine($"Theta Angle: {groundStation.ThetaAngle}");
                writer.WriteLine($"Field of View Angle: {groundStation.FieldOfViewAngle}");
            }
        }

        // Method to write ground station report to a file
        public void WriteGroundStationReportToFile(GroundStation groundStation, string groundStationReportFilePath, string message)
        {
            try
            {
                bool fileExists = File.Exists(groundStationReportFilePath);

                // Write ground station report to the file
                using (StreamWriter writer = new StreamWriter(groundStationReportFilePath, true)) // true: append mode so it allows to write data without overwriting 
                {
                    if (!fileExists)
                    {
                        writer.WriteLine("Ground Station Report:");
                        writer.WriteLine($"Ground Station Name: {groundStation.GroundStationName}");
                    }
                    writer.WriteLine($"Message: {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to write satellite report to a file
        public void WriteSatelliteReportToFile(Satellite satellite, string satelliteFolderPath, string message)
        {
            try
            {
                bool fileExists = File.Exists(satelliteFolderPath);

                // Write satellite report to the file
                using (StreamWriter writer = new StreamWriter(satelliteFolderPath, true)) // true: append mode so it allows to write data without overwriting  
                {
                    if (!fileExists)
                    {
                        writer.WriteLine("Satellite Report:");
                        writer.WriteLine($"Satellite Name: {satellite.Properties.SatelliteName}");
                    }
                    writer.WriteLine($"Message: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Method to write simulation data to a file
        public void WriteSimulationDataToFile(double initialTime, double[] X, double thetaAngle, double betaAngle, StreamWriter writer)
        {
            // Write simulation data to the file
            writer.Write(initialTime + "   ");
            for (int i = 0; i < X.Length; i++) writer.Write(X[i] + "   ");
            writer.Write((double)thetaAngle * 180 / Math.PI + "   ");
            writer.Write((double)betaAngle * 180 / Math.PI);
            writer.WriteLine();
        }

    }

    #endregion

}

