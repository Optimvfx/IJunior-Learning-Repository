using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RigedbodyExtenstions
{
    public static IEnumerable<RaycastHit2D> GetProjectionContacts(this Rigidbody2D rigidbody, Vector2 move, uint hitBufferLength, ContactFilter2D contactFilter)
    {
        return GetProjectionContacts(rigidbody, move, hitBufferLength, contactFilter, 0);
    }
    public static IEnumerable<RaycastHit2D> GetProjectionContacts(this Rigidbody2D rigidbody, Vector2 move, uint hitBufferLength, ContactFilter2D contactFilter, float shellRadius)
    {
        shellRadius = Mathf.Max(shellRadius, 0);

        float distance = move.magnitude;

        var hitBuffer = new RaycastHit2D[hitBufferLength];

        int count = rigidbody.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

        return hitBuffer.Take(count).ToList();
    }
}
