<template src="./UserTests.html"></template>
<script>
import QuestInput from "@/components/QuestInput";
import { mapActions, mapGetters } from "vuex";
export default {
  data() {
    return {
      QuestCounter: 1,
      AddedQuestCounter: 0,
      NewTest: { name: null },
      NewQuests: [],
      testValid: false,
      QuestLength: null,
    };
  },
  methods: {
    ...mapActions(["postNewTest", "loadTests"]),
    async CreateTest() {
      if (this.testValid) {
        await this.postNewTest({
          test: this.NewTest,
          quests: this.NewQuests,
          user: this.CurrUser,
        });
        await this.loadTests();
        this.$bvModal.hide("modal-scoped");
      }
    },
    onSubmit(event) {
      event.preventDefault();
    },
    checkQuest(quests, id, valid) {
      this.NewQuests[id] = quests;
      this.testValid = valid;
    },
    AnswerValidation(valid) {
      this.testValid = valid;
    },
    delQuest() {
      this.QuestCounter--;
      this.NewQuests.pop();
    },
    TestValid() {
      if (this.NewTest.name === "" || this.NewTest.name === null) {
        return false;
      } else {
        return true;
      }
    },
  },
  computed: {
    ...mapGetters(["currUser"]),
    CurrUser() {
      return this.currUser;
    },
    QuestLen() {
      return this.NewQuests.length;
    },
  },
  watch: {
    NewTest: {
      handler: function(newVal, oldVal) {
        this.testValid = this.TestValid();
        this.AnswerValidation();
      },
      deep: true,
    },
  },
  components: {
    QuestInput,
  },
};
</script>
<style scoped src="./UserTests.css"></style>
