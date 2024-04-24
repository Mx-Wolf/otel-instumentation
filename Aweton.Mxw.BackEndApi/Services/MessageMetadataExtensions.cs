using DotPulsar;

internal static class MessageMetadataExtensions{
  public static MessageMetadata AddProperty(this MessageMetadata self, string name, string? value){
    if(!string.IsNullOrWhiteSpace(value)){
      self[name] = value;
    }
    return self;
  }
}