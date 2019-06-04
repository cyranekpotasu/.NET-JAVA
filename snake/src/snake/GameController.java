package snake;

import javafx.application.Platform;
import javafx.event.ActionEvent;
import javafx.event.Event;
import javafx.event.EventType;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.StackPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

public class GameController implements Initializable {

    private final static GameController gameController = new GameController();
    public static GameController getInstance(){return gameController;}
    public Thread thread;

    @FXML
    private Canvas canvas;
    @FXML
    private AnchorPane anchorPane;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        canvas.setFocusTraversable(true);
        GameLoop loop = new GameLoop(this, 1);
        thread = new Thread(loop);
        thread.start();
        synchronized (thread) {
            try {
            DeadDialog();
        } catch (IOException e) {
            e.printStackTrace();
        }
        }
    }



    public Canvas getCanvas() {
        return canvas;
    }

    public void DeadDialog()throws IOException {


                System.out.println("You are Here!");


                    final Stage dialog = new Stage();
                    dialog.initModality(Modality.APPLICATION_MODAL);
                    Parent root = FXMLLoader.load(getClass().getResource("Lose.fxml"));
                    //Stage stage = (Stage) anchorPane.getScene().getWindow();
                   // dialog.initOwner(stage);
                    dialog.setScene(new Scene(root, 327, 147));
                    dialog.setResizable(false);
                    dialog.show();
            }

    private Stage getStage (Event event){
        return (Stage) ((Node) event.getSource()).getScene().getWindow();
    }
}
