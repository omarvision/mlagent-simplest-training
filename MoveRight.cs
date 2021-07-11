using UnityEngine;
using Unity.MLAgents;

public class MoveRight : Agent
{
    public float Movespeed = 20;
    private Vector3 orig;
    private TextMesh txtInfo = null;
    private Bounds bndFloor;

    public override void Initialize()
    {
        orig = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        txtInfo = this.transform.parent.transform.Find("txtInfo").GetComponent<TextMesh>();
        bndFloor = this.transform.parent.transform.Find("Floor").GetComponent<Renderer>().bounds;
    }
    public override void OnEpisodeBegin()
    {
        Globals.Episode += 1;
        this.transform.position = new Vector3(orig.x, orig.y, orig.z);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        this.transform.Translate(Vector3.right * vectorAction[0] * Movespeed * Time.deltaTime);
        this.transform.Translate(Vector3.forward * vectorAction[1] * Movespeed * Time.deltaTime);

        BoundCheck();
        ScreenText(ref vectorAction);
    }
    private void BoundCheck()
    {
        if (this.transform.position.x > bndFloor.max.x)
        {
            Globals.Success += 1;
            AddReward(1.0f);
            EndEpisode();
        }
        else if (this.transform.position.x < bndFloor.min.x)
        {
            Globals.Fail += 1;
            AddReward(-0.1f);
            EndEpisode();
        }

        if (this.transform.position.z > bndFloor.max.z)
        {
            Globals.Fail += 1;
            AddReward(-0.1f);
            EndEpisode();
        }
        else if (this.transform.position.z < bndFloor.min.z)
        {
            Globals.Fail += 1;
            AddReward(-0.1f);
            EndEpisode();
        }
    }
    private void ScreenText(ref float[] vectorAction)
    {
        Globals.ScreenText();
        txtInfo.text = string.Format("vectorAction\n{0}\n{1}"
            , vectorAction[0].ToString(Globals.NUMBER_FORMAT)
            , vectorAction[1].ToString(Globals.NUMBER_FORMAT));
    }
}
