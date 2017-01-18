#pragma strict


function OnTriggerEnter (obj : Collider) {
	var thedoor = gameObject.FindWithTag("SF_Door");
	thedoor.GetComponent.<Animation>().Play("openL");
}

function OnTriggerExit (obj : Collider) {
	var thedoor = gameObject.FindWithTag("SF_Door");
		thedoor.GetComponent.<Animation>().Play("closeL");
	
}