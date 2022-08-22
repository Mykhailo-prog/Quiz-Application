<template src="./Start.html"></template>
<script>
import QuestList from "@/components/QuestList";
import { mapState, mapActions } from "vuex";
export default {
  name: "Quiz",
  data() {
    return {
      quests: [],
      answers: [],
      btnStatus: false,
      finishStatus: false,
      currUserScore: 0,
      currQuest: 0,
      pugCounter: 0,
    };
  },
  methods: {
    Back() {
      this.$router.push("/tests");
    },
    nextq() {
      if (this.currQuest < this.$store.state.questions.questList.length - 1) {
        this.currQuest += 1;
      }
    },
    prevq() {
      if (this.currQuest > 0) {
        this.currQuest -= 1;
      }
    },
    getQuest(param) {
      this.quests = param;
    },
    changeQuest(param) {
      this.currQuest = param;
      this.btnStatus = !this.btnStatus;
    },
    checkReady(ans) {
      this.answers = ans;
      if (this.quests.length == ans.length) {
        this.finishStatus = true;
        return true;
      } else {
        this.finishStatus = false;
        return false;
      }
    },
    updateUser() {
      for (const i in this.answers) {
        if (this.answers[i] === this.quests[i].correctAnswer) {
          this.currUserScore += 1;
        }
      }
      this.$store.dispatch("updateUserScore", this.currUserScore);
      this.currUserScore = 0;
      this.$router.push("/");
      this.$store.dispatch("loadUsers");
    },
  },
  computed: {
    ...mapState({
      questions: (state) => state.questions.questList,
      count: (state) => state.questions.pugCounter,
    }),
    GetQuestion() {
      return this.questions;
    },
  },
  mounted() {},
  components: {
    QuestList,
  },
};
</script>
<style scoped src="./Start.css"></style>
