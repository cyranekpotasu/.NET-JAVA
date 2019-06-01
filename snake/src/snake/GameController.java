package snake;

import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.canvas.Canvas;

import java.net.URL;
import java.util.ResourceBundle;

public class GameController implements Initializable {
    @FXML
    private Canvas canvas;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        canvas.setFocusTraversable(true);

        GameLoop loop = new GameLoop(this, 3);
        (new Thread(loop)).start();
    }

    public Canvas getCanvas() {
        return canvas;
    }
}
