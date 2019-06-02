package snake;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

import java.util.LinkedList;
import java.util.List;

public class Snake implements Paintable {
    private static final Color COLOR = Color.FORESTGREEN;

    private List<Vec2D> body;
    private int speedFactor;
    private Vec2D direction;

    Snake(Vec2D initial, final int speedFactor) {
        body = new LinkedList<>();
        body.add(initial);

        this.speedFactor = speedFactor;
        direction = new Vec2D(0, 0);
    }

    public void update() {
        Vec2D head = body.get(body.size() - 1);
        body.add(head.translate(speedFactor * direction.getX(),
                speedFactor * direction.getY()));
        body.remove(0);
    }

    public void setDirection(final Vec2D direction) {
        if (body.size() > 1) {
            if ((direction.getX() != 0 && direction.getX() == -this.direction.getX())
                    || (direction.getY() != 0 && direction.getY() == -this.direction.getY()))
                return;
        }
        this.direction = direction;
    }

    @Override
    public void paint(GraphicsContext gc) {
        gc.setFill(COLOR);
        for (Vec2D point : body) {
            gc.fillRect(point.getX(), point.getY(), SQUARE_SIZE, SQUARE_SIZE);
        }
    }
}
