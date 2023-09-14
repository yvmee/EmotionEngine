using System;
using UnityEngine;
using TMPro;

namespace EmotionEngine
{

    public class SpringModel : MonoBehaviour
    {
        // Fixed Springnode:Story 0 ~ Spring 0 ~ Springnode:Personality 1 ~ Spring 1 ~ Springnode:Emotion 2
        private SpringNode storyNode = new SpringNode();
        private SpringNode personalityNode = new SpringNode();
        private SpringNode emotionNode = new SpringNode();
        private Spring springStoryPersonality = new Spring();
        private Spring springPersonalityEmotion = new Spring();

        [SerializeField] private Vector6 personality;

        [SerializeField] private double freezeThreshold = 0.01d;
        
        public double adjustments = 1f;
        public double storyNodeFactor = 5f;
        
        public static EmotionStimulusEvent EmotionEvent = new();
        public static ChangePersonality ChangePersonalityEvent = new();
        
        private EmotionState _emotionStateState;

        // Debug
        public TextMeshProUGUI text;
        
        // Dimensions
        // x = Joy,  y = Sadness, z = Anger,  u = Fear,  v = Surprise,  w = Pride

        private void Start()
        {
            storyNode.Position = new Vector6(0, 0, 0, 0, 0, 0);
            storyNode.Velocity = new Vector6(0, 0, 0, 0, 0, 0);
            storyNode.mass = 1d;
            personalityNode.Position = new Vector6(0.2, 0.2, 0.2, 0.2, 0.2, 0.2);
            ResetPersonalityNode();
            personalityNode.Velocity = new Vector6(0, 0, 0, 0, 0, 0);
            personalityNode.mass = 1d;
            emotionNode.Position = new Vector6(1, 0, 0, 0, 1, 0);
            emotionNode.Velocity = new Vector6(0, 0, 0, 0, 0, 0);
            emotionNode.mass = 1d;
            
            // connect personality and emotion
            springPersonalityEmotion.dr = 0.1d;
            springPersonalityEmotion.s = 2d;
            springPersonalityEmotion.node0 = personalityNode;
            springPersonalityEmotion.node1 = emotionNode;
            
            // connect story and personality
            springStoryPersonality.dr = 0.1d;
            springStoryPersonality.s = 2d;
            springStoryPersonality.node0 = storyNode;
            springStoryPersonality.node1 = personalityNode;
            
            _emotionStateState = ScriptableObject.CreateInstance<EmotionState>();
            EmotionEvent.AddListener(EmotionStimulus);
            ChangePersonalityEvent.AddListener(ProcessPersonalityEvent);
        }

        private void ProcessPersonalityEvent(PersonalityEvent personalityEvent)
        {
            personality.x = personalityEvent.tendencies.GetEmotion(EmotionType.Joy).Intensity;
            personality.y = personalityEvent.tendencies.GetEmotion(EmotionType.Sadness).Intensity;
            personality.z = personalityEvent.tendencies.GetEmotion(EmotionType.Anger).Intensity;
            personality.u = personalityEvent.tendencies.GetEmotion(EmotionType.Fear).Intensity;
            personality.v = personalityEvent.tendencies.GetEmotion(EmotionType.Surprise).Intensity;
            personality.w = personalityEvent.tendencies.GetEmotion(EmotionType.Pride).Intensity;
            ResetPersonalityNode();
            Unfreeze();
        }

        private void FixedUpdate()
        {
            Decay();
        }
        
        public void EmotionStimulus(EmotionStimulus emotionStimulus)
        {
            Unfreeze();
            if (emotionStimulus.storyEvent) RaiseStoryEmotionEvent(emotionStimulus);
            else RaiseEmotionEvent(emotionStimulus);
        }

        private void Unfreeze()
        {
            springPersonalityEmotion.freeze = false;
            springStoryPersonality.freeze = false;
            emotionNode.Velocity = new Vector6(0, 0, 0, 0, 0, 0);
            personalityNode.Velocity = new Vector6(0, 0, 0, 0, 0, 0);
            storyNode.Velocity = new Vector6(0, 0, 0, 0, 0, 0);
        }


        public void RaiseStoryEmotionEvent(EmotionStimulus emotionStimulus)
        {
            SetStoryNode(emotionStimulus.emotionState);
            SetSpringNode(emotionStimulus.emotionState, emotionNode);
        }
        
        public void RaiseEmotionEvent(EmotionStimulus emotionStimulus)
        {
            ResetPersonalityNode();
            SetSpringNode(emotionStimulus.emotionState, emotionNode);
            SetStoryNode(emotionStimulus.emotionState);
        }
        
        
        public void SpringCalculation()
        {
            if (springStoryPersonality.freeze && springPersonalityEmotion.freeze)
                return;
            personalityNode.Position.Add(personalityNode.Velocity.Copy().Multiply(Time.fixedDeltaTime * adjustments));
            emotionNode.Position.Add(emotionNode.Velocity.Copy().Multiply(Time.fixedDeltaTime * adjustments));

            // Calculate new Velocities
            if (!springStoryPersonality.freeze)
                CalculateVelocities(springStoryPersonality);
            if(!springPersonalityEmotion.freeze)
                CalculateVelocities(springPersonalityEmotion);
        }

        private void CalculateVelocities(Spring spring)
        {
            // Step 1: Calculate distance d from node1 -> node0
            var direction = spring.node0.Position.Copy();
            direction.Subtract(spring.node1.Position);
            var distance = direction.Magnitude();

            if (distance < freezeThreshold)
            {
                spring.freeze = true;
                return;
            }

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
        }

        public void SetStoryNode(EmotionState emotionState)
        {
            storyNode.Position.x = emotionState.GetEmotion(EmotionType.Joy).Intensity / storyNodeFactor;
            storyNode.Position.y = emotionState.GetEmotion(EmotionType.Sadness).Intensity / storyNodeFactor;
            storyNode.Position.z = emotionState.GetEmotion(EmotionType.Anger).Intensity / storyNodeFactor;
            storyNode.Position.u = emotionState.GetEmotion(EmotionType.Fear).Intensity / storyNodeFactor;
            storyNode.Position.v = emotionState.GetEmotion(EmotionType.Surprise).Intensity / storyNodeFactor;
            storyNode.Position.w = emotionState.GetEmotion(EmotionType.Pride).Intensity / storyNodeFactor;
        }
        
        private void ResetPersonalityNode()
        {
            personalityNode.Position.x = personality.x;
            personalityNode.Position.y = personality.y;
            personalityNode.Position.z = personality.z;
            personalityNode.Position.u = personality.u;
            personalityNode.Position.v = personality.v;
            personalityNode.Position.w = personality.w;
        }

        private void SetSpringNode(EmotionState emotionState, SpringNode node)
        {
            node.Position.x = emotionState.GetEmotion(EmotionType.Joy).Intensity;
            node.Position.y = emotionState.GetEmotion(EmotionType.Sadness).Intensity;
            node.Position.z = emotionState.GetEmotion(EmotionType.Anger).Intensity;
            node.Position.u = emotionState.GetEmotion(EmotionType.Fear).Intensity;
            node.Position.v = emotionState.GetEmotion(EmotionType.Surprise).Intensity;
            node.Position.w = emotionState.GetEmotion(EmotionType.Pride).Intensity;
        }



        public void Decay()
        {
            SpringCalculation();
            ClampPos();
            text.text = "Personality Node Pos: \n" + (float) personalityNode.Position.x + ",\n" + (float) personalityNode.Position.y + ",\n" + (float) personalityNode.Position.z +
                        ",\n" + (float) personalityNode.Position.u + ",\n" + (float) personalityNode.Position.v + ",\n" + (float) personalityNode.Position.w + ",\n" +
            "Emotion Node Pos: \n" + (float) emotionNode.Position.x + ",\n" + (float) emotionNode.Position.y + ",\n" + (float) emotionNode.Position.z +
                ",\n" + (float) emotionNode.Position.u + ",\n" + (float) emotionNode.Position.v + ",\n" + (float) emotionNode.Position.w + ",\n"; 
        }
        
        private void ClampPos()
        {
            emotionNode.Position.x = Math.Min(1, Math.Max(emotionNode.Position.x, 0));
            emotionNode.Position.y = Math.Min(1, Math.Max(emotionNode.Position.y, 0));
            emotionNode.Position.z = Math.Min(1, Math.Max(emotionNode.Position.z, 0));
            emotionNode.Position.u = Math.Min(1, Math.Max(emotionNode.Position.u, 0));
            emotionNode.Position.v = Math.Min(1, Math.Max(emotionNode.Position.v, 0));
            emotionNode.Position.w = Math.Min(1, Math.Max(emotionNode.Position.w, 0));
            
            personalityNode.Position.x = Math.Min(1, Math.Max(personalityNode.Position.x, 0));
            personalityNode.Position.y = Math.Min(1, Math.Max(personalityNode.Position.y, 0));
            personalityNode.Position.z = Math.Min(1, Math.Max(personalityNode.Position.z, 0));
            personalityNode.Position.u = Math.Min(1, Math.Max(personalityNode.Position.u, 0));
            personalityNode.Position.v = Math.Min(1, Math.Max(personalityNode.Position.v, 0));
            personalityNode.Position.w = Math.Min(1, Math.Max(personalityNode.Position.w, 0));
        }
    }
}
