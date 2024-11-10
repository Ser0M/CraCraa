using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure you have this for UI components
using TMPro;
using UnityEngine.SceneManagement;

public class TriviaManager : MonoBehaviour
{
    public TMP_Text questionText;
    public Button[] answerButtons;
    public TMP_Text playerHPText; // Text to show player's HP
    public TMP_Text enemyHPText; // Text to show enemy's HP
    public GameObject gameOverScreen; // Assign this in the Inspector
    public GameObject playerGameObject;
    public Slider playerHPSlider; // Slider for player's HP
    public Slider enemyHPSlider;

    private string correctAnswer;

    // Player and enemy HP
    private int playerHP = 100;
    private int enemyHP = 100;

    private const int damageOnIncorrect = 20; // Damage for incorrect answer

    private List<Question> triviaQuestions = new List<Question>
    {
        new Question("What is the capital of France?", "Paris", new string[] { "London", "Berlin", "Rome" }),
        new Question("What is 2 + 2?", "4", new string[] { "3", "5", "6" }),
        new Question("Who wrote 'Hamlet'?", "William Shakespeare", new string[] { "Charles Dickens", "Jane Austen", "Mark Twain" }),
        new Question("What is the chemical symbol for water?", "H2O", new string[] { "CO2", "O2", "N2" }),
        new Question("Who painted the Mona Lisa?", "Leonardo da Vinci", new string[] { "Pablo Picasso", "Vincent van Gogh", "Claude Monet" }),
        new Question("Which planet is known as the Red Planet?", "Mars", new string[] { "Venus", "Jupiter", "Saturn" }),
        new Question("What is the largest ocean on Earth?", "Pacific Ocean", new string[] { "Atlantic Ocean", "Indian Ocean", "Arctic Ocean" }),
        new Question("What is the boiling point of water in Celsius?", "100°C", new string[] { "90°C", "110°C", "120°C" }),
        new Question("Who discovered penicillin?", "Alexander Fleming", new string[] { "Marie Curie", "Isaac Newton", "Louis Pasteur" }),
        new Question("Which element has the atomic number 1?", "Hydrogen", new string[] { "Oxygen", "Helium", "Nitrogen" }),
        new Question("What is the tallest mountain in the world?", "Mount Everest", new string[] { "K2", "Kangchenjunga", "Mount Kilimanjaro" }),
        new Question("What is the largest mammal?", "Blue Whale", new string[] { "Elephant", "Giraffe", "Whale Shark" }),
        new Question("What is the main ingredient in guacamole?", "Avocado", new string[] { "Tomato", "Onion", "Cucumber" }),
        new Question("Which country is known as the Land of the Rising Sun?", "Japan", new string[] { "China", "Korea", "Thailand" }),
        new Question("What is the longest river in the world?", "Amazon River", new string[] { "Nile River", "Yangtze River", "Mississippi River" }),
        new Question("Who was the first president of the United States?", "George Washington", new string[] { "Thomas Jefferson", "Abraham Lincoln", "John Adams" }),
        new Question("Which animal is known as the King of the Jungle?", "Lion", new string[] { "Tiger", "Elephant", "Cheetah" }),
        new Question("What is the square root of 64?", "8", new string[] { "6", "7", "9" }),
        new Question("Which is the smallest country in the world?", "Vatican City", new string[] { "Monaco", "Nauru", "San Marino" }),
        new Question("What is the currency of Japan?", "Yen", new string[] { "Won", "Ringgit", "Rupee" }),
        new Question("Which sea is located between Saudi Arabia and Africa?", "Red Sea", new string[] { "Mediterranean Sea", "Dead Sea", "Black Sea" }),
        new Question("In which year did the Titanic sink?", "1912", new string[] { "1905", "1923", "1898" }),
        new Question("Which company is known for the iPhone?", "Apple", new string[] { "Samsung", "Nokia", "Microsoft" }),
        new Question("What is the capital of Italy?", "Rome", new string[] { "Paris", "Berlin", "Madrid" }),
        new Question("Who invented the telephone?", "Alexander Graham Bell", new string[] { "Thomas Edison", "Nikola Tesla", "Michael Faraday" }),
        new Question("Which planet is closest to the sun?", "Mercury", new string[] { "Venus", "Earth", "Mars" }),
        new Question("Which is the fastest land animal?", "Cheetah", new string[] { "Lion", "Tiger", "Gazelle" }),
        new Question("Which organ in the human body produces insulin?", "Pancreas", new string[] { "Liver", "Heart", "Kidneys" }),
        new Question("What is the national sport of Canada?", "Ice Hockey", new string[] { "Basketball", "Football", "Baseball" }),
        new Question("Who wrote '1984'?", "George Orwell", new string[] { "Aldous Huxley", "Ray Bradbury", "J.R.R. Tolkien" }),
        new Question("In what year did World War II end?", "1945", new string[] { "1939", "1950", "1940" }),
        new Question("What is the most common blood type?", "O+", new string[] { "A+", "B-", "AB+" }),
        new Question("What is the capital of Australia?", "Canberra", new string[] { "Sydney", "Melbourne", "Brisbane" }),
        new Question("What is the largest desert in the world?", "Sahara Desert", new string[] { "Kalahari Desert", "Gobi Desert", "Atacama Desert" }),
        new Question("What is the chemical symbol for gold?", "Au", new string[] { "Ag", "Fe", "Pb" }),
        new Question("Which bird is known for its ability to mimic human speech?", "Parrot", new string[] { "Crow", "Eagle", "Owl" }),
        new Question("What is the capital of Canada?", "Ottawa", new string[] { "Toronto", "Vancouver", "Montreal" }),
        new Question("Which sea creature has three hearts?", "Octopus", new string[] { "Shark", "Whale", "Dolphin" }),
        new Question("What is the tallest statue in the world?", "Statue of Unity", new string[] { "Christ the Redeemer", "Liberty Statue", "Motherland Calls" }),
        new Question("In which country would you find the Great Barrier Reef?", "Australia", new string[] { "United States", "Mexico", "Indonesia" }),
        new Question("What is the hardest natural substance on Earth?", "Diamond", new string[] { "Gold", "Iron", "Platinum" }),
        new Question("What is the currency of the United Kingdom?", "Pound Sterling", new string[] { "Euro", "Yen", "Dollar" }),
        new Question("Who was the first man to walk on the moon?", "Neil Armstrong", new string[] { "Buzz Aldrin", "Yuri Gagarin", "Michael Collins" }),
        new Question("Which is the longest running TV show?", "The Simpsons", new string[] { "Friends", "Family Guy", "South Park" }),
        new Question("What is the largest planet in our solar system?", "Jupiter", new string[] { "Saturn", "Neptune", "Earth" }),
        new Question("Which element is represented by the symbol 'O'?", "Oxygen", new string[] { "Osmium", "Ozone", "Opium" }),
        new Question("Who was the first woman to fly in space?", "Valentina Tereshkova", new string[] { "Sally Ride", "Mae Jemison", "Amelia Earhart" }),
        new Question("What is the name of the longest river in Africa?", "Nile River", new string[] { "Congo River", "Amazon River", "Zambezi River" }),
        new Question("What is the main ingredient in hummus?", "Chickpeas", new string[] { "Lentils", "Beans", "Rice" }),
        new Question("Who is known as the Father of Modern Physics?", "Albert Einstein", new string[] { "Isaac Newton", "Marie Curie", "Niels Bohr" }),
        new Question("Which planet has the most moons?", "Jupiter", new string[] { "Saturn", "Mars", "Venus" }),
        new Question("Which country is home to the Eiffel Tower?", "France", new string[] { "Italy", "Germany", "Spain" }),
        new Question("What does a seismograph measure?", "Earthquakes", new string[] { "Tornadoes", "Volcanic Eruptions", "Floods" }),
        new Question("What is the smallest planet in our solar system?", "Mercury", new string[] { "Mars", "Venus", "Earth" }),
        new Question("What is the largest island in the world?", "Greenland", new string[] { "Australia", "New Guinea", "Borneo" }),
        new Question("Who is the author of 'Harry Potter'?", "J.K. Rowling", new string[] { "George R.R. Martin", "J.R.R. Tolkien", "Suzanne Collins" }),
        new Question("What is the symbol for the element carbon?", "C", new string[] { "O", "N", "H" }),
        new Question("What is the largest continent?", "Asia", new string[] { "Africa", "North America", "Europe" }),
        new Question("Who was the first emperor of China?", "Qin Shi Huang", new string[] { "Liu Bang", "Tang Taizong", "Emperor Wu" }),
        new Question("What is the capital of Germany?", "Berlin", new string[] { "Munich", "Hamburg", "Frankfurt" }),
        new Question("Which artist is famous for cutting off his own ear?", "Vincent van Gogh", new string[] { "Pablo Picasso", "Leonardo da Vinci", "Claude Monet" }),
        new Question("Which bird is the symbol of peace?", "Dove", new string[] { "Eagle", "Owl", "Phoenix" }),
        new Question("What is the currency of the United States?", "Dollar", new string[] { "Euro", "Pound", "Yen" }),
        new Question("What is the most spoken language in the world?", "Mandarin", new string[] { "English", "Spanish", "Hindi" }),
        new Question("What is the longest-running animated TV show?", "The Simpsons", new string[] { "Family Guy", "South Park", "Futurama" }),
        new Question("What is the capital of Spain?", "Madrid", new string[] { "Barcelona", "Sevilla", "Valencia" }),
        new Question("Who was the first African American president of the United States?", "Barack Obama", new string[] { "Martin Luther King Jr.", "Malcolm X", "Jesse Jackson" }),
        new Question("Which company created the first computer mouse?", "Xerox", new string[] { "Apple", "Microsoft", "IBM" }),
        new Question("What is the largest country in the world?", "Russia", new string[] { "Canada", "United States", "China" }),
        new Question("Which element has the symbol 'Fe'?", "Iron", new string[] { "Gold", "Copper", "Silver" }),
        new Question("What is the capital of Mexico?", "Mexico City", new string[] { "Guadalajara", "Cancún", "Monterrey" }),
        new Question("Who was the first man in space?", "Yuri Gagarin", new string[] { "Neil Armstrong", "Buzz Aldrin", "John Glenn" }),
        new Question("What is the largest type of bear?", "Polar Bear", new string[] { "Grizzly Bear", "Black Bear", "Brown Bear" }),
        new Question("What is the largest volcano on Earth?", "Mauna Loa", new string[] { "Mount Everest", "Mount Fuji", "Mount St. Helens" }),
        new Question("Who was the first woman to win a Nobel Prize?", "Marie Curie", new string[] { "Rosalind Franklin", "Dorothy Crowfoot Hodgkin", "Ada Lovelace" }),
        new Question("What is the only continent without a desert?", "Europe", new string[] { "Australia", "Africa", "Antarctica" }),
        new Question("Which is the largest hot desert in the world?", "Sahara", new string[] { "Gobi", "Kalahari", "Atacama" }),
        new Question("What country is known as the Land of the Free?", "United States", new string[] { "Canada", "Australia", "India" }),
        new Question("Which country invented pizza?", "Italy", new string[] { "France", "Germany", "United States" }),
        new Question("Which animal is the largest land mammal?", "Elephant", new string[] { "Rhinoceros", "Hippo", "Giraffe" }),
        new Question("What is the national flower of Japan?", "Cherry Blossom", new string[] { "Rose", "Tulip", "Lotus" }),
        new Question("Which ocean is the largest?", "Pacific Ocean", new string[] { "Atlantic Ocean", "Indian Ocean", "Arctic Ocean" }),
        new Question("Which river flows through Egypt?", "Nile River", new string[] { "Amazon River", "Yangtze River", "Ganges River" }),
        new Question("What is the longest mountain range in the world?", "Andes", new string[] { "Himalayas", "Rockies", "Alps" }),
        new Question("What is the capital of Brazil?", "Brasília", new string[] { "Rio de Janeiro", "São Paulo", "Buenos Aires" }),
        new Question("What is the tallest building in the world?", "Burj Khalifa", new string[] { "Shanghai Tower", "One World Trade Center", "Taipei 101" }),
    new Question("What is the chemical symbol for potassium?", "K", new string[] { "P", "Pt", "Po" }),
    new Question("Who wrote 'To Kill a Mockingbird'?", "Harper Lee", new string[] { "F. Scott Fitzgerald", "Ernest Hemingway", "Mark Twain" }),
    new Question("What is the smallest country by population?", "Vatican City", new string[] { "Monaco", "San Marino", "Liechtenstein" }),
    new Question("What sport is known as the 'king of sports'?", "Soccer", new string[] { "Basketball", "Tennis", "Cricket" }),
    new Question("What is the national animal of Scotland?", "Unicorn", new string[] { "Lion", "Dragon", "Stag" }),
    new Question("Which gas is most abundant in the Earth's atmosphere?", "Nitrogen", new string[] { "Oxygen", "Carbon Dioxide", "Argon" }),
    new Question("What year did the Berlin Wall fall?", "1989", new string[] { "1991", "1987", "1990" }),
    new Question("Who developed the theory of general relativity?", "Albert Einstein", new string[] { "Isaac Newton", "Nikola Tesla", "Galileo Galilei" }),
    new Question("What is the hottest planet in the solar system?", "Venus", new string[] { "Mercury", "Mars", "Jupiter" }),
    new Question("What is the currency of South Korea?", "Won", new string[] { "Yen", "Baht", "Peso" }),
    new Question("What element does 'Na' represent on the periodic table?", "Sodium", new string[] { "Nitrogen", "Nickel", "Neon" }),
    new Question("Who painted the Sistine Chapel ceiling?", "Michelangelo", new string[] { "Leonardo da Vinci", "Raphael", "Caravaggio" }),
    new Question("Which is the longest bone in the human body?", "Femur", new string[] { "Humerus", "Tibia", "Fibula" }),
    new Question("What is the capital of South Korea?", "Seoul", new string[] { "Busan", "Incheon", "Gwangju" }),
    new Question("What is the most widely spoken language in South America?", "Spanish", new string[] { "Portuguese", "English", "French" }),
    new Question("Who was the first female Prime Minister of the UK?", "Margaret Thatcher", new string[] { "Theresa May", "Indira Gandhi", "Angela Merkel" }),
    new Question("What does DNA stand for?", "Deoxyribonucleic Acid", new string[] { "Dioxygenated Nucleic Acid", "Dimethyl Amino Acid", "Dehydrated Acid" }),
    new Question("What was the first country to grant women the right to vote?", "New Zealand", new string[] { "Australia", "Finland", "Canada" }),
    new Question("What is the hardest rock on Earth?", "Diamond", new string[] { "Quartz", "Granite", "Topaz" }),
    new Question("Which planet is known for its rings?", "Saturn", new string[] { "Jupiter", "Neptune", "Uranus" }),
    new Question("Who discovered America?", "Christopher Columbus", new string[] { "Leif Erikson", "Marco Polo", "Vasco da Gama" }),
    new Question("What is the largest type of deer?", "Moose", new string[] { "Elk", "White-tailed Deer", "Red Deer" }),
    new Question("What is the national dish of Spain?", "Paella", new string[] { "Tacos", "Sushi", "Curry" }),
    new Question("What is the main language spoken in Brazil?", "Portuguese", new string[] { "Spanish", "English", "French" }),
    new Question("What was the first animal to be sent into space?", "Dog", new string[] { "Monkey", "Cat", "Mouse" }),
    new Question("What element has the highest melting point?", "Tungsten", new string[] { "Iron", "Platinum", "Carbon" }),
    new Question("Which country is known for tulips and windmills?", "Netherlands", new string[] { "Belgium", "Denmark", "Germany" }),
    new Question("Who directed 'Jaws'?", "Steven Spielberg", new string[] { "Martin Scorsese", "James Cameron", "Ridley Scott" }),
    new Question("What is the rarest blood type?", "AB-", new string[] { "O-", "B+", "A+" }),
    new Question("Which ocean is the deepest?", "Pacific Ocean", new string[] { "Atlantic Ocean", "Indian Ocean", "Arctic Ocean" }),
    new Question("What is the main ingredient in traditional Japanese miso soup?", "Miso paste", new string[] { "Tofu", "Soy sauce", "Rice" }),
    new Question("Who invented the World Wide Web?", "Tim Berners-Lee", new string[] { "Bill Gates", "Steve Jobs", "Linus Torvalds" }),
    new Question("What is the national sport of Japan?", "Sumo wrestling", new string[] { "Judo", "Karate", "Baseball" }),
    new Question("What is the largest lake in Africa?", "Lake Victoria", new string[] { "Lake Tanganyika", "Lake Malawi", "Lake Chad" }),
    new Question("What color are aircraft black boxes?", "Orange", new string[] { "Black", "Yellow", "Red" }),
    new Question("What year did the first manned moon landing occur?", "1969", new string[] { "1967", "1971", "1965" }),
    new Question("What is the official language of Egypt?", "Arabic", new string[] { "French", "English", "Swahili" }),
    new Question("What is the currency of Brazil?", "Real", new string[] { "Peso", "Dollar", "Euro" }),
    new Question("What is the symbol for potassium on the periodic table?", "K", new string[] { "P", "Pt", "Po" }),
    new Question("Which animal is known as the 'Ship of the Desert'?", "Camel", new string[] { "Elephant", "Horse", "Donkey" }),
    new Question("What was the first feature-length animated movie?", "Snow White and the Seven Dwarfs", new string[] { "Cinderella", "Pinocchio", "Fantasia" }),
    new Question("Which planet is known for having the Great Red Spot?", "Jupiter", new string[] { "Mars", "Saturn", "Neptune" }),
    new Question("Who is the Greek god of the sea?", "Poseidon", new string[] { "Zeus", "Hades", "Hermes" }),
    new Question("What is the name of the city that is home to the Acropolis?", "Athens", new string[] { "Sparta", "Rome", "Delphi" }),
    new Question("What is the smallest prime number?", "2", new string[] { "1", "3", "5" }),
    new Question("What is the capital of Argentina?", "Buenos Aires", new string[] { "Santiago", "Lima", "Montevideo" }),
    new Question("Who developed the polio vaccine?", "Jonas Salk", new string[] { "Albert Sabin", "Alexander Fleming", "Edward Jenner" }),
    new Question("What does HTTP stand for?", "Hypertext Transfer Protocol", new string[] { "Hypertext Transmission Protocol", "Hyperlink Transfer Protocol", "Hyper Transfer Text Protocol" })
    };

    private void Start()
    {
        if (questionText == null)
        {
            Debug.LogError("questionText is not assigned!");
        }
        if (answerButtons == null || answerButtons.Length == 0)
        {
            Debug.LogError("answerButtons are not assigned!");

        }
        // Initialize the sliders
        if (playerHPSlider != null)
        {
            playerHPSlider.maxValue = playerHP;
            playerHPSlider.value = playerHP;
        }
        if (enemyHPSlider != null)
        {
            enemyHPSlider.maxValue = enemyHP;
            enemyHPSlider.value = enemyHP;
        }

        // Update the HP UI to show initial values
        UpdateHP();
        DisplayRandomQuestion();
    }

    private void UpdateHP()
    {
        // Update the HP display text
        playerHPText.text = "Player HP: " + playerHP;
        enemyHPText.text = "Enemy HP: " + enemyHP;

        // Update slider values
        if (playerHPSlider != null)
        {
            playerHPSlider.value = playerHP;
        }
        if (enemyHPSlider != null)
        {
            enemyHPSlider.value = enemyHP;
        }

        // Check for game over conditions
        if (playerHP <= 0)
        {
            playerHP = 0;
            playerHPText.text = "HP: " + playerHP;
            playerHPSlider.value = playerHP;

            // Set the player inactive and show the Game Over screen
            playerGameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
        else if (enemyHP <= 0)
        {
            Debug.Log("Enemy has been defeated!");
            // Handle enemy defeat (win, next level, etc.)
            SceneManager.LoadScene(0);

        }

    }
    private void OnPlayerWin()
    {
        // Your code that handles when the player wins

        PlayerPrefs.SetInt("DuelWon", 1); // Set the "DuelWon" key
        PlayerPrefs.Save(); // Ensure it is saved
    }



    public void OnRestartButtonClicked()
    {
        // Reactivate the player and hide the Game Over screen
        playerGameObject.SetActive(true);
        gameOverScreen.SetActive(false);

        // Reset the player's HP and update the UI
        playerHP = 100; // Or whatever the starting HP is
        playerHPText.text = "HP: " + playerHP;
        enemyHP = 100;
        enemyHPText.text = "HP: " + enemyHP;

        // Update the HP UI to reflect the reset values
        UpdateHP();
    }
    



    private void DisplayRandomQuestion()
    {
        // Ensure that there are trivia questions available
        if (triviaQuestions.Count == 0)
        {
            Debug.LogError("No trivia questions available.");
            return;
        }

        Question selectedQuestion = triviaQuestions[Random.Range(0, triviaQuestions.Count)];

        questionText.text = selectedQuestion.QuestionText;

        List<string> answers = new List<string>(selectedQuestion.IncorrectAnswers) { selectedQuestion.CorrectAnswer };
        answers.Shuffle();

        // Assign answers to the buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Get the TMP_Text component inside the button
            TMP_Text buttonText = answerButtons[i].GetComponentInChildren<TMP_Text>();

            // Check if button text is found
            if (buttonText != null)
            {
                buttonText.text = answers[i]; // Set the button's text to the answer

                int index = i;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerClicked(answers[index], selectedQuestion.CorrectAnswer));
            }
            else
            {
                Debug.LogError($"No TMP_Text found in button {i}. Make sure the button has a child with TMP_Text.");
            }
        }

        correctAnswer = selectedQuestion.CorrectAnswer;
    }

    private void OnAnswerClicked(string selectedAnswer, string correctAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Correct Answer!");
            enemyHP -= 20; // Enemy takes damage for incorrect answer
        }
        else
        {
            Debug.Log("Incorrect Answer.");
            playerHP -= damageOnIncorrect; // Player takes damage for incorrect answer
        }

        UpdateHP(); // Update the HP display and check for death

        DisplayRandomQuestion(); // Proceed to the next question after answering
    }
    public void GoBack()
    {

        SceneManager.LoadScene(0);

    }
}

[System.Serializable]
public class Question
{
    public string QuestionText;
    public string CorrectAnswer;
    public string[] IncorrectAnswers;

    public Question(string questionText, string correctAnswer, string[] incorrectAnswers)
    {
        QuestionText = questionText;
        CorrectAnswer = correctAnswer;
        IncorrectAnswers = incorrectAnswers;
    }
}

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}