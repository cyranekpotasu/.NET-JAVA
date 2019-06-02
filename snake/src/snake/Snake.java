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
    private boolean dead = false;

    Snake(Vec2D initial, final int speedFactor) {
        body = new LinkedList<>();
        body.add(initial);

        this.speedFactor = speedFactor;
        direction = new Vec2D(0, 0);
    }

    public void update() {
        grow();
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

    public Vec2D getHead() {
        return body.get(body.size() - 1);
    }

    public void setHead(final Vec2D newHead) {
        body.set(body.size() - 1, newHead);
    }

    public void grow() {
        Vec2D newHead = getNextHead();
        dead = body.size() > 1 && body.contains(newHead);
        body.add(newHead);
    }

    public int length() {
        return body.size();
    }

    public boolean isDead() {
        return dead;
    }

    @Override
    public void paint(GraphicsContext gc) {
        gc.setFill(COLOR);
        for (Vec2D point : body) {
            gc.fillRect(SQUARE_SIZE * point.getX(), SQUARE_SIZE * point.getY(),
                    SQUARE_SIZE, SQUARE_SIZE);
        }
        if (dead) {
            gc.setFill(Color.CRIMSON);
            gc.fillRect(SQUARE_SIZE * getHead().getX(), SQUARE_SIZE * getHead().getY(),
                    SQUARE_SIZE, SQUARE_SIZE);
        }
    }

    private Vec2D getNextHead() {
        return getHead().translate(speedFactor * direction.getX(),
                speedFactor * direction.getY());
    }
}
