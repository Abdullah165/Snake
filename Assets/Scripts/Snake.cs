using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    Vector2 direction = Vector2.right;
    [SerializeField] float snakeSpeed = 10f;


    List<Transform> segments;
    [SerializeField] Transform snakeSegmentsPrefab;

    int score;
    int bestScore;
    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;

    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);

        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        bestScoreText.text = bestScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.Translate(direction * snakeSpeed * Time.deltaTime);
    }


    void Grow()
    {

        // segment.position = segments[segments.Count - 1].position;
        for (int index = 0; index <= 4; index++)
        {
            Transform segment = Instantiate(snakeSegmentsPrefab);
            segment.position = segments[segments.Count - 1].position;
            segments.Add(segment);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Grow();
            audio.Play();
            score += 10;
            scoreText.text = score.ToString();
            if (bestScore < score)
                PlayerPrefs.SetInt("bestScore", score);
        }
        else if (collision.gameObject.CompareTag("Obstacles"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
