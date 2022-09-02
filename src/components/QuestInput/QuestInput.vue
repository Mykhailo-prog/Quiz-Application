<template src="./QuestInput.html"></template>
<script>
import AnswerInput from "@/components/AnswerInput";
export default {
  props: {
    counter: {
      type: Number,
    },
  },
  data() {
    return {
      QuestVisible: true,
      AnsCounter: 1,
      NewQuest: {
        question: null,
        correctAnswer: null,
        testId: null,
        NewAnswers: [],
      },
      Answer: null,
    };
  },
  methods: {
    addAns(answer, id) {
      answer.questionId = this.counter;
      this.Answer = answer;
      this.NewQuest.NewAnswers[id] = answer;
      this.QuestValid();
    },
    delAns() {
      this.AnsCounter--;
      this.NewAnswers.pop();
    },
    QuestValid() {
      if (
        this.NewQuest.question === (null, "") ||
        this.NewQuest.correctAnswer === (null, "")
      ) {
        return false;
      } else {
        return true;
      }
    },
    AnsValid() {
      if (this.NewQuest.NewAnswers.length === 0) {
        return false;
      }
      let cnt = 0;
      this.NewQuest.NewAnswers.forEach((ans) => {
        if (ans.answer === this.NewQuest.correctAnswer) {
          cnt++;
        }
      });
      if (cnt != 1) {
        return false;
      } else {
        return true;
      }
    },
  },
  watch: {
    NewQuest: {
      handler: function(newVal, oldVal) {
        this.$emit(
          "added-quest",
          newVal,
          this.counter - 1,
          this.AnsValid() ? this.QuestValid() : false
        );
      },
      immediate: true,
      deep: true,
    },
    Answer: {
      handler: function(newVal) {
        this.$emit("ans-valid", this.AnsValid());
      },
      deep: true,
    },
  },
  components: {
    AnswerInput,
  },
};
</script>
<style scoped src="./QuestInput.css"></style>
