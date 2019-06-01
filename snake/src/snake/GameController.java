package snake;

import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;

import java.net.URL;
import java.util.ResourceBundle;

public class GameController implements Initializable {
    @FXML
    private Canvas canvas;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        canvas.setFocusTraversable(true);

        GameLoop loop = new GameLoop(this);
        (new Thread(loop)).start();
    }

    public Canvas getCanvas() {
        return canvas;
    }
}
