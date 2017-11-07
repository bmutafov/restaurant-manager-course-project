using System.Collections.Generic;

public class ActiveDeliverySources : GenericSingletonClass<ActiveDeliverySources>
{
    public List<DeliverySource> deliverySources;

    private void Start ()
    {
        foreach ( var source in deliverySources )
        {
            DayCycle.Instance.onDayChangedCallback += source.GenerateDaily;
        }
        DayCycle.Instance.onDayChangedCallback += DisplayInfo;
    }

    private void DisplayInfo ()
    {
        foreach ( var source in deliverySources )
        {
            source.DisplayOffer();
        }
    }

}
