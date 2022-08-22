<template src="./QuestList.html"></template>
<script>
import questions from "@/store/questions";
import Pugination from "@/components/Pugination";
import { mapState, mapActions } from "vuex";
export default {
  props: {
    question: {
      type: Object,
      required: true,
      default: () => {},
    },
  },
  data() {
    return {
      usersAnswer: "",
      currQuest: {},
    };
  },
  methods: {
    ...mapActions(["getUserAnswer", "check"]),

    getAnswer(answer) {
      this.usersAnswer = answer.ans;
      this.getUserAnswer({
        answer: this.usersAnswer,
        index: this.getAnswerPos,
      });
      this.check(this.getQuests);
    },
    checkFinish() {
      this.$emit("checkFin", this.Answers);
    },
  },
  computed: {
    ...mapState({
      quests: (state) => state.questions.questList,
    }),
    getAnswerPos() {
      return this.quests.indexOf(this.currQuest);
    },
    getQuests() {
      return this.quests;
    },
  },
  components: {
    Pugination,
  },
};
</script>
<style scoped src="./QuestList.css"></style>
