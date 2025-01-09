<template>
  <Suspense>
    <v-container fluid>
      <v-row>
        <v-col>
          <v-btn prepend-icon="$plus" @click="addModel()"> Добавить </v-btn>
        </v-col>
      </v-row>

      <v-row v-if="editingValue">
        <v-col>
          <v-card>
            <v-card-title>
              <span class="text-h5">Редактирование</span>
            </v-card-title>
            <v-card-text>
              <v-row>
                <v-col>
                  <v-date-input v-model="editingValue.date" label="Дата"></v-date-input>
                </v-col>
                <v-col>
                  <v-text-field v-model="editingValue.name" label="Название"></v-text-field>
                </v-col>
                <v-col>
                  <v-text-field v-model="editingValue.description" label="Описание"></v-text-field>
                </v-col>
                <v-col>
                  <v-number-input
                    v-model="editingValue.recommendedAmount"
                    label="Рекомендуемая сумма"
                    suffix="₽"
                    :min="0"
                    :step="100"
                  ></v-number-input>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <div style="display: flex; gap: 10px">
                    <v-btn v-if="editingValue.id" color="primary" @click="saveModel()">Сохранить</v-btn>
                    <v-btn v-if="!editingValue.id" color="primary" @click="createRegular()">Обычный</v-btn>
                    <v-btn v-if="!editingValue.id" color="primary" @click="createPeriodic()">Периодический</v-btn>
                    <v-btn v-if="!editingValue.id" color="primary" @click="createTargeted()">Целевой</v-btn>
                    <v-btn color="error" @click="editingValue = null">Отменить</v-btn>
                  </div>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <v-table>
            <thead>
              <tr>
                <th />
                <th class="text-left">ID</th>
                <th class="text-left">Дата</th>
                <th class="text-left">Название</th>
                <th class="text-left">Рек. сумма</th>
                <th class="text-left">Тип</th>
                <th class="text-left">Закрыт</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="model in fundraisings" :key="model.id">
                <td>
                  <v-icon icon="$edit" @click="editingValue = model" />
                  <v-icon icon="$delete" @click="deleteModel(model)" />
                </td>
                <td>{{ model.id }}</td>
                <td>{{ model.date }}</td>
                <td>{{ model.name }}</td>
                <td>{{ model.recommendedAmount }}</td>
                <td>{{ model.type }}</td>
                <td>{{ model.isClosed }}</td>
              </tr>
            </tbody>
          </v-table>
        </v-col>
      </v-row>
    </v-container>
    <template #fallback> Загрузка... </template>
  </Suspense>
</template>

<script lang="ts" setup>
import { onBeforeMount, ref } from "vue";
import { Fund } from "@/models/fund";
import { Fundraising, FundraisingType } from "@/models/fundraising";
import { CrudService } from "@/services/crud-service";
import { FundraisingsService } from "@/services/fundraisings-service";
import { FundraisingEditDto } from "@/models/fundraising-edit-dto";

const fundraisings = ref<Fundraising[]>([]);
const funds = ref<Fund[]>([]);
const service = new FundraisingsService();
const fundsService = new CrudService<Fund>("fund");
const editingValue = ref<FundraisingEditDto | null>(null);

onBeforeMount(async () => {
  funds.value = await fundsService.getAll();
  fundraisings.value = await service.getAll();
});

function addModel(): void {
  editingValue.value = {
    name: "",
    description: "",
    date: new Date(),
    recommendedAmount: 1000,
    type: FundraisingType.OneTime,
  };
}

async function saveModel(): Promise<void> {
  const model = editingValue.value!;
  await service.update(model as Fundraising);
  editingValue.value = null;
}

async function createRegular(): Promise<void> {
  await service.createRegular(editingValue.value!);
  editingValue.value = null;
}

async function createPeriodic(): Promise<void> {
  await service.createPeriodic(editingValue.value!);
  editingValue.value = null;
}

async function createTargeted(): Promise<void> {
  await service.createTargeted(editingValue.value!);
  editingValue.value = null;
}

async function deleteModel(model: Fundraising): Promise<void> {
  await service.delete(model.id);
  fundraisings.value = fundraisings.value.filter((s) => s.id !== model.id);
}
</script>
