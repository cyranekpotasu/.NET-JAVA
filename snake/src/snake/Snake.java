package snake;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

import java.util.LinkedList;
import java.util.List;

public class Snake implements Paintable {
    private static final Color COLOR = Color.FORESTGREEN;

    private List<Vec2D> body;
    private Vec2D velocity;

    Snake(Vec2D initial) {
        body = new LinkedList<>();
        body.add(initial);

        velocity = new Vec2D(0, 0);
    }

    @Override
    public void paint(GraphicsContext gc) {
        gc.setFill(COLOR);
        for (Vec2D point : body) {
            gc.fillRect(point.getX(), point.getY(), SQUARE_SIZE, SQUARE_SIZE);
        }
    }
}
