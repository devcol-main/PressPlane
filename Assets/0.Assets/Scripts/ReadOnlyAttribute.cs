using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true)]

public class ReadOnlyAttribute : PropertyAttribute { }

// ReadOnlyAttribute.cs (place anywhere except Editor folder)
// can be empty