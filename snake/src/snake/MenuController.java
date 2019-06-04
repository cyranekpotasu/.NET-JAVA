package snake;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

import javafx.event.ActionEvent;
import javafx.event.Event;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.input.MouseEvent;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class MenuController implements Initializable
{
    private Settings settings = Settings.getInstance();
    GameController gameController = GameController.getInstance();

                               /*START Difficulty*/

    @FXML
    private void startSPgameEASY (ActionEvent event) throws IOException{
        settings.setDifficulty("easy");
        startNewGame(event);
    }
    @FXML
    private void startSPgameMEDIUM (ActionEvent event) throws IOException{
        settings.setDifficulty("medium");
        startNewGame(event);

    }
    @FXML
    private void startSPgameHARD (ActionEvent event) throws IOException{
        settings.setDifficulty("hard");
        startNewGame(event);
    }

    private void startNewGame(ActionEvent event) throws IOException{
        Stage app_stage = getStage(event);
        //Start new singleplayer game
        handleSceneChange("game.fxml", app_stage, 600, 600);

    }


    @FXML
    private void backToMain (ActionEvent event) throws IOException{
        Stage app_stage = getStage(event);
        handleSceneChange("menu.fxml", app_stage, 600, 600);
    }

                                    /*END Difficulty*/
                                    /*Start Lose*/

     @FXML
      public void btnReset_click(ActionEvent actionEvent) throws IOException{
         Stage app_stage = getStage(actionEvent);
         //Start new singleplayer game
         handleSceneChange("game.fxml", app_stage, 600, 600);
     }

                                  /*END Lose*/

    @FXML
    private void btnSP_click(ActionEvent event) throws IOException {
        Stage app_stage = getStage(event);
        handleSceneChange("Difficulty.fxml", app_stage, 600, 600);
    }


    @FXML
    private void btnMP_Click(final ActionEvent event)
    {
        provideAboutFunctionality();
    }

    @FXML
    private void btnSettings_Click(final ActionEvent event)
    {
        provideAboutFunctionality();
    }


    @FXML
    private void btnLogout(final MouseEvent event)
    {
        provideAboutFunctionality();
        Stage app_stage = getStage(event);
        app_stage.close();
    }



    @FXML
    private void btnRanking_Click(final ActionEvent event)
    {
        provideAboutFunctionality();
    }


    private void provideAboutFunctionality()
    {
        System.out.println("You clicked on About!");
    }

    private Stage getStage (Event event){
        return (Stage) ((Node) event.getSource()).getScene().getWindow();
    }

    private Node getNode (Event event){
        return (Node) event.getSource();
    }

    public void handleSceneChange(String fxml, Stage stage, int width, int height) throws IOException {
        Parent root = FXMLLoader.load(getClass().getResource(fxml));

        stage.setScene(new Scene(root, width, height));
        stage.setResizable(false);
        stage.show();
    }

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {

    }


}
