namespace EimaFunctions.RequestModels;

internal class SlackModels
{
}

public class SlackRequest
{
    public string Token { get; set; }
    public string Type { get; set; }
}

public class SlackUrlVerificationRequest : SlackRequest
{
    public string Token { get; set; }
    public string Challenge { get; set; }
    public string Type { get; set; }
}


public class SlackUrlVerificationResponse
{
    public string Challenge { get; set; }
}


public class SlackMessage : SlackRequest
{
    public string token { get; set; }
    public string type { get; set; }
    public string team_id { get; set; }
    public string context_team_id { get; set; }
    public object context_enterprise_id { get; set; }
    public string api_app_id { get; set; }
    public Event _event { get; set; }
    public string event_id { get; set; }
    public int event_time { get; set; }
    public Authorization[] authorizations { get; set; }
    public bool is_ext_shared_channel { get; set; }
    public string event_context { get; set; }
}

public class Event
{
    public string subtype { get; set; }
    public string text { get; set; }
    public string user { get; set; }
    public string bot_link { get; set; }
    public string bot_id { get; set; }
    public string type { get; set; }
    public string ts { get; set; }
    public string channel { get; set; }
    public string event_ts { get; set; }
    public string channel_type { get; set; }
}

public class Authorization
{
    public object enterprise_id { get; set; }
    public string team_id { get; set; }
    public string user_id { get; set; }
    public bool is_bot { get; set; }
    public bool is_enterprise_install { get; set; }
}




public class SlackEvent
{
    public string Type { get; set; }
    public string? Challenge { get; set; }
    public SlackInnerEvent? Event { get; set; }
}

public class SlackInnerEvent
{
    public string Type { get; set; }
    public string? User { get; set; }
    public string? Text { get; set; }
    // Add other properties as needed for different event types
}