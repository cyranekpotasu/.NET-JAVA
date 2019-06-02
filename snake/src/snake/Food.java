package snake;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

public class Food implements Paintable {
    private static final Color color = Color.RED;

    private Vec2D location;

    Food(final Vec2D location) {
        this.location = location;
    }

    Vec2D getLocation() {
        return location;
    }

    @Override
    public void paint(GraphicsContext gc) {
        gc.setFill(color);
        gc.fillRect(location.getX() * SQUARE_SIZE, location.getY() * SQUARE_SIZE,
                SQUARE_SIZE, SQUARE_SIZE);
    }
}
