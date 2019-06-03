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
import javafx.stage.Modality;
import javafx.stage.Stage;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

public class GameController implements Initializable {

    Thread thread;

    @FXML
    private Canvas canvas;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        canvas.setFocusTraversable(true);

        GameLoop loop = new GameLoop(this, 1);
        thread = new Thread(loop) ;
        thread.start();
        /* Tu trzeb jeszcze ogarnąć
        while (true){
            if(!thread.isAlive())
                DeadDialog(ActionEvent.ACTION);
            ;
        }*/
    }

    private void DeadDialog(EventType<ActionEvent> action) {
    }

    public Canvas getCanvas() {
        return canvas;
    }

    public void DeadDialog(ActionEvent event)throws IOException {
        Stage app_stage = getStage(event);
        final Stage dialog = new Stage();
        dialog.initModality(Modality.APPLICATION_MODAL);
        Parent root = FXMLLoader.load(getClass().getResource("Paused.fxml"));
        dialog.initOwner(app_stage);
        dialog.setScene(new Scene(root, 327, 147));
        dialog.setResizable(false);
        dialog.show();
    }

    private Stage getStage (Event event){
        return (Stage) ((Node) event.getSource()).getScene().getWindow();
    }
}
