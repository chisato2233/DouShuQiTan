using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


namespace DouShuQiTan {
    public static class RandomTools {
        public static bool Probability(float p) {
            if(p <= 0 || Random.value < p) 
                return false;
            return true;
        }


        public static List<int> UniqueRandomNumbers(int start, int end, int count) {
            List<int> numbers = new List<int>();
            for (int i = start; i < end; i++) {
                numbers.Add(i);
            }

            // 洗牌算法来随机排列数字
            int n = numbers.Count;
            while (n > 1) {
                n--;
                int k = Random.Range(0, n + 1);
                (numbers[k], numbers[n]) = (numbers[n], numbers[k]);
            }

            // 取前count个数字
            return numbers.GetRange(0, Math.Min(numbers.Count,count));
        }

        public static int ChooseIndexByProbability(params float[] probabilities) {
            float total = 0f;
            foreach (float probability in probabilities) {
                total += probability;
            }

            float randomPoint = Random.value * total; // Random.value 返回 0 到 1 之间的随机数

            float cumulative = 0f;
            for (int i = 0; i < probabilities.Length; i++) {
                cumulative += probabilities[i];
                if (randomPoint < cumulative) {
                    return i;
                }
            }

            return probabilities.Length - 1; // 防止所有概率之和小于1的情况
        }
    }
}
