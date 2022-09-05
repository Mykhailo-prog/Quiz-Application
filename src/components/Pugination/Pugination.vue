<template src="./Pugination.html"></template>
<script>
import { mapActions, mapState, mapGetters, mapMutations } from "vuex";
export default {
  methods: {
    ...mapActions({
      Inc: "increaseCounter",
      Dec: "decreaseCounter",
      Set: "getCounter",
    }),

    Prev() {
      this.Dec();
    },
    Next() {
      this.Inc();
    },
    setCount(count) {
      this.Set(count);
    },
    async goToResult() {
      this.$router.push("/result");
    },
  },
  computed: {
    ...mapState({
      quests: (state) => state.questions.questList,
      answers: (state) => state.answers.answerList,
      checkParam: (state) => state.answers.checkFinish,
      counter: (state) => state.questions.pugCounter,
    }),
  },
};
</script>
<style scoped src="./Pugination.css"></style>
