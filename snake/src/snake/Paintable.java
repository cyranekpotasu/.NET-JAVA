package snake;

import javafx.scene.canvas.GraphicsContext;

public interface Paintable {
    int SQUARE_SIZE = 15;

    void paint(GraphicsContext gc);
}
