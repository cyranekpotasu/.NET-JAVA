package snake;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

import java.util.Random;

public class Grid implements Paintable {
    private static final Color BG_COLOR = new Color(0.1, 0.1, 0.1, 1);

    private final int rows;
    private final int cols;

    private Snake snake;
    private Food food;

    Grid(final double width, final double height, Snake snake) {
        cols = (int) width / SQUARE_SIZE;
        rows = (int) height / SQUARE_SIZE;

        this.snake = snake;
        food = new Food(getRandomPoint());
    }

    public void update() {
        snake.update();
        snake.setHead(wrap(snake.getHead()));
        if (snake.getHead().equals(food.getLocation())) {
            snake.grow();
            food = new Food(getRandomPoint());
        }
    }

    public Vec2D wrap(Vec2D position) {
        int x = position.getX(), y = position.getY();
        if (x < 0)
            x = rows + position.getX();
        if (y < 0)
            y = cols + position.getY();
        return new Vec2D(x % rows, y % cols);
    }

    @Override
    public void paint(GraphicsContext gc) {
        gc.setFill(BG_COLOR);
        gc.fillRect(0, 0, rows * SQUARE_SIZE, cols * SQUARE_SIZE);
        snake.paint(gc);
        food.paint(gc);
    }

    public Snake getSnake() {
        return snake;
    }

    private Vec2D getRandomPoint() {
        while (true) {
            Random random = new Random();
            if(snake.getBody().contains(random))
                continue;
            return new Vec2D(random.nextInt(rows), random.nextInt(cols));
        }
    }
}
