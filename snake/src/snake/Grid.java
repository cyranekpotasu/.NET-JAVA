package snake;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

public class Grid implements Paintable {
    private static final Color BG_COLOR = new Color(0.1, 0.1, 0.1, 1);

    private final int rows;
    private final int cols;

    Grid(final double width, final double height) {
        cols = (int) width / SQUARE_SIZE;
        rows = (int) height / SQUARE_SIZE;
    }

    @Override
    public void paint(GraphicsContext gc) {
        gc.setFill(BG_COLOR);
        gc.fillRect(0, 0, rows * SQUARE_SIZE, cols * SQUARE_SIZE);
    }
}
