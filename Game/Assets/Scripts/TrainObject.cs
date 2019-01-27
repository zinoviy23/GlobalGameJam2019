using System;
using System.Collections;
using System.Collections.Generic;
using Interactive;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainObject : ButtonInteractiveObject
{
    private float speed = 0.0f;
    
    // Start is called before the first frame update
    protected override IEnumerator Interact()
    {
        StartInteracting();
        speed = 10.0f;

        yield return new WaitForSeconds(0.1f);
        
        triggerObject.GetComponent<SimpleWalker>().enabled = false;

        GetComponent<SpriteRenderer>().flipX = true;

        Wrapper wrapper = new Wrapper();
        StartCoroutine(AnimateTriggerMoving(triggerObject, new Vector2(10.56f, 1.51f), wrapper));
        
        yield return new WaitWhile(() => wrapper.value);
        
        Vector3 target = new Vector2(13.56f, 1.51f);
        
        float step = speed * Time.deltaTime;

        while (transform.position != target)
        {
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            yield return null;
        }

        SceneManager.LoadScene(3);
        
        FinishInteracting();
    }
    
    IEnumerator AnimateTriggerMoving(GameObject obj, Vector3 point, Wrapper wrapper = null)
    {
        if (wrapper != null)
            wrapper.value = true;
        
        SimpleWalker walker;
        walker = obj.GetComponent<SimpleWalker>();
        while (Math.Abs(obj.transform.position.x - point.x) > 0.01)
        {
            Vector3 moveResult = Vector3.MoveTowards(
                new Vector3(obj.transform.position.x, 0),
                new Vector3(point.x, 0),
                5 * Time.fixedDeltaTime);

            obj.transform.position = new Vector3(moveResult.x, obj.transform.position.y);

            
            if (walker != null)
            {
                if (point.x - obj.transform.position.x > 0.01)
                    walker.Flip(false);
                else if (point.x - obj.transform.position.x < -0.01)
                    walker.Flip(true);
            }
            
            yield return new WaitForFixedUpdate();
        }

        if (wrapper != null)
            wrapper.value = false;
    }
    
    class Wrapper
    {
        public bool value = true;
    }
}
