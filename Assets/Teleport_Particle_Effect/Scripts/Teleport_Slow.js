var TeleportVideoParticles : ParticleSystem;
var SmokeParticles : ParticleSystem;
var SparkParticles : ParticleSystem;
var TeleportLight: Light;
var TeleportAudio : AudioSource;

 private var fadeStart = 10;
 private var fadeEnd = 0;
 private var fadeTime = 4.6;
 private var t = 0.0;

function Update ()
{
   
   if (Input.GetButtonDown("Fire1")) //check to see if the left mouse was pushed.
  
    {
       TeleportVideoParticles.Play();
       SmokeParticles.Play();
       SparkParticles.Play();
       TeleportAudio.Play();


       FadeLight();
      
     }
       
   
}



function FadeLight()
{
   
  
              while (t < fadeTime) 
              
              {
               t += Time.deltaTime;
               
               TeleportLight.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
               yield;
               
              }              
            
t = 0;
   
   
}

