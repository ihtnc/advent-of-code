namespace AdventOfCode.App.Y2022.Problems.Day3;

[Flags]
public enum Letters : long
{
    Blank = 0b0000_0000L,

    A = 0b0000_0001L,
    B = 0b0000_0010L,
    C = 0b0000_0100L,
    D = 0b0000_1000L,
    E = 0b0001_0000L,
    F = 0b0010_0000L,
    G = 0b0100_0000L,
    H = 0b1000_0000L,

    I = 0b0000_0001L << 8,
    J = 0b0000_0010L << 8,
    K = 0b0000_0100L << 8,
    L = 0b0000_1000L << 8,
    M = 0b0001_0000L << 8,
    N = 0b0010_0000L << 8,
    O = 0b0100_0000L << 8,
    P = 0b1000_0000L << 8,

    Q = 0b0000_0001L << 16,
    R = 0b0000_0010L << 16,
    S = 0b0000_0100L << 16,
    T = 0b0000_1000L << 16,
    U = 0b0001_0000L << 16,
    V = 0b0010_0000L << 16,
    W = 0b0100_0000L << 16,
    X = 0b1000_0000L << 16,

    Y = 0b0000_0001L << 24,
    Z = 0b0000_0010L << 24,
    a = 0b0000_0100L << 24,
    b = 0b0000_1000L << 24,
    c = 0b0001_0000L << 24,
    d = 0b0010_0000L << 24,
    e = 0b0100_0000L << 24,
    f = 0b1000_0000L << 24,

    g = 0b0000_0001L << 32,
    h = 0b0000_0010L << 32,
    i = 0b0000_0100L << 32,
    j = 0b0000_1000L << 32,
    k = 0b0001_0000L << 32,
    l = 0b0010_0000L << 32,
    m = 0b0100_0000L << 32,
    n = 0b1000_0000L << 32,

    o = 0b0000_0001L << 40,
    p = 0b0000_0010L << 40,
    q = 0b0000_0100L << 40,
    r = 0b0000_1000L << 40,
    s = 0b0001_0000L << 40,
    t = 0b0010_0000L << 40,
    u = 0b0100_0000L << 40,
    v = 0b1000_0000L << 40,

    w = 0b0000_0001L << 48,
    x = 0b0000_0010L << 48,
    y = 0b0000_0100L << 48,
    z = 0b0000_1000L << 48,
}