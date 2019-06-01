package snake;

import javafx.event.EventHandler;
import javafx.scene.input.KeyEvent;

public class KeyHandler implements EventHandler<KeyEvent> {
    private final Grid grid;

    KeyHandler(final Grid grid) {
        this.grid = grid;
    }

    @Override
    public void handle(KeyEvent keyEvent) {
        Snake snake = grid.getSnake();
        switch (keyEvent.getCode()) {
            case UP:
                snake.setDirection(new Vec2D(0, -1));
                break;
            case DOWN:
                snake.setDirection(new Vec2D(0, 1));
                break;
            case LEFT:
                snake.setDirection(new Vec2D(-1, 0));
                break;
            case RIGHT:
                snake.setDirection(new Vec2D(1, 0));
                break;
        }
    }
}
