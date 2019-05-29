package snake;

import javafx.scene.canvas.GraphicsContext;

public interface Paintable {
    int SQUARE_SIZE = 10;

    void paint(GraphicsContext gc);
}
