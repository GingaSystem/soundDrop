using UnityEngine;

public class Example2 : MonoBehaviour
{
    public AudioSpectrum spectrum;
    public Transform[] cubes;
    public float scale;

    private void Update()
    {
        for ( int i = 0; i < cubes.Length; i++ )
        {
            var cube = cubes[ i ];
            var localScale = cube.localScale;
		var localPosition = cube.localPosition;
		//var color = cube.
            
  	
		localScale.y = spectrum.Levels[ i ] * scale;
		localPosition.x = spectrum.Levels[ i ] * scale;
		
	    
            cube.localScale = localScale;
        }
    }
}