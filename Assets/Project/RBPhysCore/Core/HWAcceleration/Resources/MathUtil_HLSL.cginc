
float Sign101(float x)
{
    return x > 0 ? 1 : x == 0 ? 0 : -1;
}

float Sign11(float x)
{
    return x > 0 ? 1 : -1;
}

float Sign101E(float x, float epsilon)
{
    return abs(x) < epsilon ? 0 : x > 0 ? 1 : -1;
}