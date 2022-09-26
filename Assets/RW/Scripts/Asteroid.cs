/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 1;
    public float maxSpeed = 10;
    private float maxY = -5;

    private void Start()
    {
        StartCoroutine("ManageAsteroidSpeedUp");
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < maxY)
        {
            float changeOfSurivival = Random.Range(1, 11);

            if (changeOfSurivival <= 7)
            {
                float xPos = Random.Range(-8.0f, 8.0f);
                transform.position = new Vector3(xPos, 7.35f, 0);
            }

            else
            {
                Destroy(gameObject);
            }
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision.body.name
        if (collision.collider.name == "Shield")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Ship>().ToggleShield(false);
        }

        if (collision.collider.name == "ShipModel")
        {
            Game.GameOver();
            Destroy(gameObject);
        }
    }

    IEnumerator ManageAsteroidSpeedUp()
    {
        yield return new WaitForSeconds(1.0f);

        if (speed < maxSpeed)
        {
            speed += 0.5f;
            StartCoroutine("ManageAsteroidSpeedUp");
            Debug.Log(speed);
        }

    }

}
