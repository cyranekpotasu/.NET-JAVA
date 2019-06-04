package snake;

import javafx.application.Platform;
import javafx.event.ActionEvent;
import javafx.event.Event;
import javafx.event.EventHandler;
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
import javafx.stage.WindowEvent;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

public class GameController implements Initializable {

    private final static GameController gameController = new GameController();
    public static GameController getInstance(){return gameController;}
    static GameLoop loop;

    @FXML
    private Canvas canvas;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        canvas.setFocusTraversable(true);
        loop = new GameLoop(this, 1);
        loop.start();
    }



    public Canvas getCanvas() {
        return canvas;
    }

    void StartGameSingn(Stage app_stage)
    {
        Thread dead = new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                synchronized (loop) {
                    try {
                        loop.wait();
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                    Platform.runLater(() -> {
                        try {
                            DeadDialog(app_stage);
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    });
                }
            }
        });
        dead.start();

    }
    public void DeadDialog(Stage app_stage)throws IOException {


                System.out.println("You are Here!");

                    final Stage dialog = new Stage();
                    dialog.initModality(Modality.APPLICATION_MODAL);
                    dialog.initOwner(app_stage);
        //app_stage.close();
                    Parent root = FXMLLoader.load(getClass().getResource("Lose.fxml"));


                    dialog.setScene(new Scene(root, 327, 147));
                    dialog.setResizable(false);
                    dialog.show();

    }

    private Stage getStage (Event event){
        return (Stage) ((Node) event.getSource()).getScene().getWindow();
    }
}
