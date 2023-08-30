using System;
using UnityEngine;
using TMPro;

namespace EmotionEngine
{

    public class SpringModel : MonoBehaviour, IPersonality, IMood
    {
        // Fixed Springnode:Story 0 ~ Spring 0 ~ Springnode:Personality 1 ~ Spring 1 ~ Springnode:Emotion 2
        private SpringNode storyNode = new SpringNode();
        private SpringNode personalityNode = new SpringNode();
        private SpringNode emotionNode = new SpringNode();
        private Spring springStoryPersonality = new Spring();
        private Spring springPersonalityEmotion = new Spring();

        public double adjustments = 1f;
        
        // Debug
        public TextMeshProUGUI text;
        
        // Dimensions
        // x = Joy,  y = Sadness, z = Anger,  u = Fear,  v = Surprise,  w = Pride

        private void Start()
        {
            storyNode.Position = Vector6.Zero;
            personalityNode.Position = new Vector6(2d, 4d, 0, 0, 0, 0);
            personalityNode.Velocity = new Vector6(0, 0, 0, 0, 0, 0);
            personalityNode.mass = 1d;
            emotionNode.Position = new Vector6(1d, -3d, 0, 0, 0, 0);
            emotionNode.Velocity = new Vector6(0, 0, 0, 0, 0, 0);
            emotionNode.mass = 1d;
            springPersonalityEmotion.dr = 4d;
            springPersonalityEmotion.s = 2d;
            springPersonalityEmotion.node0 = personalityNode;
            springPersonalityEmotion.node1 = emotionNode;
        }

        private void FixedUpdate()
        {
            SpringCalculation();
            text.text = "Personality P1: (" + (float) personalityNode.Position.x + "," + (float) personalityNode.Position.y +
                        ") \n Emotion P2: (" + (float) emotionNode.Position.x + "," + (float) emotionNode.Position.y + ")";
        }

        public void SpringCalculation()
        {
            // Calculate new position
            personalityNode.Position.Add(personalityNode.Velocity.Copy().Multiply(Time.fixedDeltaTime * adjustments));
            emotionNode.Position.Add(emotionNode.Velocity.Copy().Multiply(Time.fixedDeltaTime * adjustments));
            //personalityNode.Position.Add(personalityNode.Velocity.Copy().Multiply(0.5f));
            //emotionNode.Position.Add(emotionNode.Velocity.Copy().Multiply(0.5f));

            // Calculate new Velocities
            //CalculateVelocities(springStoryPersonality);
            CalculateVelocities(springPersonalityEmotion);
        }

        private void CalculateVelocities(Spring spring)
        {
            // Step 1: Calculate distance d from personality -> story
            var direction = spring.node0.Position.Copy();
            direction.Subtract(spring.node1.Position);
            var distance = direction.Magnitude();

            // Step 2: Calculate force f
            direction.Normalize();
            var scalar = spring.s * (-1) * (distance - spring.dr);
            var force0 = direction.Multiply(scalar).Copy();
            var force1 = direction.Multiply(-1).Copy();

            // Step 3: Calculate Acceleration
            var acceleration0 = force0.Multiply((1 / spring.node0.mass));
            var acceleration1 = force1.Multiply(1 / spring.node1.mass);
            
            // Step 4: Calculate new Velocity
            spring.node0.Velocity.Add(acceleration0.Multiply(Time.fixedDeltaTime * adjustments));
            spring.node1.Velocity.Add(acceleration1.Multiply(Time.fixedDeltaTime * adjustments));
            
            //spring.node0.Velocity.Add(acceleration0.Multiply(0.5f));
            //spring.node1.Velocity.Add(acceleration1.Multiply(0.5f));
        }
        
        

        DiscreteEmotion IPersonality.ProcessEmotion(EmotionEvent emotionEvent)
        {
            SetSpringNode(emotionEvent.emotion, emotionNode);
            return emotionEvent.emotion;
        }

        public void SetPersonality(PersonalityEvent personalityEvent)
        {
            throw new NotImplementedException();
        }

        DiscreteEmotion IMood.ProcessEmotion(DiscreteEmotion emotionEvent)
        {
            SetSpringNode(emotionEvent, personalityNode);
            return emotionEvent;
        }
        
        public void SetStoryNode(DiscreteEmotion emotionEvent)
        {
            SetSpringNode(emotionEvent, storyNode);
        }

        private void SetSpringNode(DiscreteEmotion emotion, SpringNode node)
        {
            DiscreteEmotion discreteEmotion = (DiscreteEmotion)emotion;
            node.Position.x = discreteEmotion.GetEmotion(EmotionType.Joy).Intensity;
            node.Position.y = discreteEmotion.GetEmotion(EmotionType.Sadness).Intensity;
            node.Position.z = discreteEmotion.GetEmotion(EmotionType.Anger).Intensity;
            node.Position.u = discreteEmotion.GetEmotion(EmotionType.Fear).Intensity;
            node.Position.v = discreteEmotion.GetEmotion(EmotionType.Surprise).Intensity;
            node.Position.w = discreteEmotion.GetEmotion(EmotionType.Pride).Intensity;
        }
        
        public DiscreteEmotion GetCurrentMood()
        {
            throw new System.NotImplementedException();
        }

        public void Decay()
        {
            throw new System.NotImplementedException();
        }
    }
}
