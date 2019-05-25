package snake;

public class Vec2D {
    private final int x;
    private final int y;

    Vec2D(final int x, final int y) {
        this.x = x;
        this.y = y;
    }

    public int getY() {
        return y;
    }

    public int getX() {
        return x;
    }

    public Vec2D translate(final int dx, final int dy) {
        return new Vec2D(x + dx, y + dy);
    }

    @Override
    public boolean equals(Object obj) {
        if (!(obj instanceof Vec2D))
            return false;
        Vec2D vector = (Vec2D) obj;
        return x == vector.x && y == vector.y;
    }

    @Override
    public String toString() {
        return x + ", " + y;
    }
}
