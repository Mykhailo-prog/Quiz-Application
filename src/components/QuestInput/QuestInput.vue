<template src="./QuestInput.html"></template>
<script>
import AnswerInput from "@/components/AnswerInput";
import validation from "@/formValidation";

import { mapActions } from "vuex";

export default {
  props: {
    editQuest: Object,
    QuestionPos: {
      type: Number,
    },
  },
  data() {
    return {
      QuestVisible: true,
      AnswerPos: 1,
      NewQuest: {
        Quest: "",
        CorrectAnswer: "",
        Answers: [],
      },
      Answer: null,
    };
  },
  methods: {
    parseQuest(quest) {
      this.AnswerPos = quest.answers.length;
      this.NewQuest.Quest = quest.quest;
      this.NewQuest.CorrectAnswer = quest.correctAnswer;
    },
    addAnswer(answer, id) {
      this.Answer = answer;
      this.NewQuest.Answers[id] = answer;
      validation.questValidation({
        quest: this.NewQuest.Quest,
        answer: this.NewQuest.CorrectAnswer,
      });
    },
    delAnswer() {
      this.AnswerPos--;
      this.NewQuest.Answers.pop();
    },
  },
  mounted() {
    if (this.editQuest) {
      this.parseQuest(this.editQuest);
    }
  },
  watch: {
    NewQuest: {
      handler: function(newVal) {
        this.$emit(
          "added-quest",
          newVal,
          this.QuestionPos - 1,
          validation.answerValidation({
            Quest: this.NewQuest,
            Answer: this.Answer,
          })
            ? validation.questValidation({
                quest: this.NewQuest.Quest,
                answer: this.NewQuest.CorrectAnswer,
              })
            : false
        );
      },
      immediate: true,
      deep: true,
    },
    Answer: {
      handler: function(newVal) {
        this.$emit(
          "ans-valid",
          validation.answerValidation({
            Quest: this.NewQuest,
            Answer: this.Answer,
          })
        );
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
