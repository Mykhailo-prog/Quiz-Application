<template src="./UserTests.html"></template>
<script>
import QuestInput from "@/components/QuestInput";
import { mapActions, mapGetters } from "vuex";
export default {
  data() {
    return {
      QuestCounter: 1,
      AddedQuestCounter: 0,
      NewTest: { name: "" },
      NewQuests: [],
      testValid: false,
    };
  },
  methods: {
    ...mapActions(["postNewTest", "loadTests"]),
    async CreateTest() {
      if (this.testValid) {
        await this.postNewTest({
          test: this.NewTest,
          quests: this.NewQuests,
          user: this.CurrentUser,
        });
        await this.loadTests();
        this.$bvModal.hide("modal-scoped");
      }
    },
    checkQuest(quests, id, valid) {
      this.NewQuests[id] = quests;
      this.testValid = valid;
    },
    AnswerValidation(valid) {
      this.testValid = valid;
      console.log(valid);
    },
    delQuest() {
      this.QuestCounter--;
      this.NewQuests.pop();
    },
    TestValid() {
      if (this.NewTest.name === "") {
        return false;
      } else {
        return true;
      }
    },
  },
  computed: {
    ...mapGetters(["CurrentUser"]),
  },
  watch: {
    NewTest: {
      handler: function() {
        this.testValid = this.TestValid();
        this.AnswerValidation();
        console.log(this.AnswerValidation());
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
